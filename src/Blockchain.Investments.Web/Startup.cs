using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CQRSlite.Config;
using CQRSlite.Bus;
using CQRSlite.Commands;
using CQRSlite.Events;
using CQRSlite.Domain;
using CQRSlite.Cache;
using Scrutor;
using Blockchain.Investments.Core;
using Blockchain.Investments.Core.Repositories;
using Blockchain.Investments.Core.WriteModel;
using Blockchain.Investments.Core.WriteModel.Handlers;
using Blockchain.Investments.Core.ReadModel;
using MongoDB.Bson.Serialization;
using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.ReadModel.Dtos;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the IConfiguration instance which AppConfig binds against.
            services.Configure<AppConfig>(Configuration);
            
            // Add application services
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IRepository<TransactionItemListDto>, MongoRepository<TransactionItemListDto>>();
            services.AddSingleton<IRepository<Account>, MongoRepository<Account>>();
            services.AddSingleton<IRepository<Asset>, MongoRepository<Asset>>();
            services.AddSingleton<IRepository<Security>, MongoRepository<Security>>();
            services.AddSingleton<IRepository<Organization>, MongoRepository<Organization>>();
            services.AddSingleton<IRepository<Period>, MongoRepository<Period>>();
            services.AddSingleton<IRepository<Transaction>, MongoRepository<Transaction>>();

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
            services.AddScoped<CQRSlite.Domain.IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));
            services.AddTransient<IReadModelFacade, ReadModelFacade>();
            
            //Scan for commandhandlers and eventhandlers
            services.Scan(scan => scan
                .FromAssemblies(typeof(TransactionCommandHandlers).GetTypeInfo().Assembly)
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
            registrar.Register(typeof(TransactionCommandHandlers));
            
            //Register Mongo
            BsonClassMap.RegisterClassMap<TransactionCreated>();

            #endregion

            // Add framework services.
            services.AddMvc();           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
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
