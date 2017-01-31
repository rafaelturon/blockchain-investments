using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using CQRSlite.Config;
using CQRSlite.Bus;
using CQRSlite.Commands;
using CQRSlite.Events;
using CQRSlite.Domain;
using CQRSlite.Cache;
using Scrutor;
using Blockchain.Investments.Api.Options;
using Blockchain.Investments.Api.Requests;
using Blockchain.Investments.Core.Infrastructure;
using Blockchain.Investments.Core.Repositories;
using Blockchain.Investments.Core.WriteModel.Handlers;
using Blockchain.Investments.Core.ReadModel;
using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.Domain;
using Blockchain.Investments.Core.ReadModel.Dtos;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using AutoMapper;
using FluentValidation.AspNetCore;

namespace Blockchain.Investments.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        private SymmetricSecurityKey _signingKey;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the IConfiguration instance which AppConfig binds against.
            services.Configure<AppConfig>(Configuration);

            // Set symmetric security key
            string securityKey = Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
            
            // Add application services
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(Configuration);
            services.AddSingleton<IRepository<BookDto>, MongoRepository<BookDto>>();
            services.AddSingleton<IRepository<AccountDto>, MongoRepository<AccountDto>>();
            services.AddSingleton<IRepository<Security>, MongoRepository<Security>>();
            services.AddSingleton<IRepository<Price>, MongoRepository<Price>>();
            services.AddSingleton<IRepository<Period>, MongoRepository<Period>>();

            #region CQRS
            services.AddMemoryCache();
            
            //Add Cqrs services
            services.AddSingleton<InProcessBus>(new InProcessBus());
            services.AddSingleton<ICommandSender>(y => y.GetService<InProcessBus>());
            services.AddSingleton<IEventPublisher>(y => y.GetService<InProcessBus>());
            services.AddSingleton<IHandlerRegistrar>(y => y.GetService<InProcessBus>());
            services.AddScoped<ISession, Session>();
            services.AddSingleton<IEventStore, MongoEventStore>();
            services.AddScoped<ICache, CQRSlite.Cache.MemoryCache>();
            services.AddScoped<IRepository>(y => new CQRSlite.Cache.CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));
            
            services.AddTransient<IReadModelFacade, ReadModelFacade>();
            
            //Scan for commandhandlers and eventhandlers
            services.Scan(scan => scan
                .FromAssemblies(typeof(BookCommandHandlers).GetTypeInfo().Assembly)
                    .AddClasses(classes => classes.Where(x => {
                        var allInterfaces = x.GetInterfaces();
                        return 
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICommandHandler<>)) ||
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IEventHandler<>));
                    }))
                    .AsSelf()
                    .WithTransientLifetime()
            );

            //Register bus
            var serviceProvider = services.BuildServiceProvider();
            var registrar = new BusRegistrar(new DependencyResolver(serviceProvider));
            registrar.Register(typeof(BookCommandHandlers));
            
            //Register Mongo
            MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;
            BsonClassMap.RegisterClassMap<TransactionCreated>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.ObjectId));
            });
            BsonClassMap.RegisterClassMap<AccountCreated>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.ObjectId));
            });
            #endregion

            // AutoMapper
            services.AddAutoMapper();
            
            // Add framework services.
            services.AddMvc(
                config =>
                {
                    // Make authentication compulsory
                    var policy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                    // Bad request filter
                    config.Filters.Add(new BadRequestActionFilter());
                }
            ).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()); // FluentValidation

            // Set up authorization policies.
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AuthorizationPolicy,
                                policy => policy.RequireClaim(Constants.ClaimType, Constants.ClaimValue));
            });
            
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //string currentUrl = app.ServerFeatures.Get<Microsoft.AspNetCore.Hosting.Server.Features.IServerAddressesFeature>().Addresses.Single();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = false,
                //ValidAudience = currentUrl,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
