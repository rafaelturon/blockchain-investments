using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Blockchain.Investments.Api.Options;
using Blockchain.Investments.Bitcoin.Domain;
using Blockchain.Investments.Core.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger<IdentityController> _logger;
        private readonly JsonSerializerSettings _serializerSettings;
        public IdentityController(IOptions<JwtIssuerOptions> jwtOptions, ILogger<IdentityController> logger) 
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
            _logger = logger;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            Guid guid = Guid.NewGuid();
            string guidString = guid.ToString().Replace ("-", "");

            long ticks = DateTime.UtcNow.Ticks;
            string nonce = guidString + ticks.ToString ("x");

            var httpContext = HttpContext;
            //string url = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString}";
            string hostName = httpContext.Request.Host.Host;
            string bitIdUri = "bitid://" + hostName + "/api/identity?x=" + nonce + "&u=1";

            BitIdRequest bitIdRequest = new BitIdRequest();
            bitIdRequest.BitIdUri = bitIdUri;
            bitIdRequest.BitIdImageQr = "https://chart.googleapis.com/chart?cht=qr&chs=400x400&chl=" + bitIdUri;
            
            // list.Add(nonce.Substring(0, 32)); // guid
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", bitIdUri);
            return new ObjectResult(bitIdRequest);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromForm]BitIdCredentials request) 
        {
            string jsonResponseToken = string.Empty;

            BitIdResponse response = request.VerifyMessage();
            jsonResponseToken = JsonConvert.SerializeObject(response, _serializerSettings);
            if (response.Success) 
            {
                // use attribute >> [Authorize(Policy = Constants.AuthorizationPolicy)]
                var identity = GetClaimsIdentity(request.Address);
                
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.Address),
                    new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                    identity.FindFirst(Constants.ClaimType)
                };
                // Create the JWT security token and encode it.
                var jwt = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    notBefore: _jwtOptions.NotBefore,
                    expires: _jwtOptions.Expiration,
                    signingCredentials: _jwtOptions.SigningCredentials);
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                // Serialize and return the response
                var responseToken = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
                };

                jsonResponseToken = JsonConvert.SerializeObject(responseToken, _serializerSettings);
                
            }

            return new ObjectResult(jsonResponseToken);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        
        private static ClaimsIdentity GetClaimsIdentity(string bitcoinPublicAddress)
        {
            return new ClaimsIdentity(new GenericIdentity(bitcoinPublicAddress, "Token"),
                new[]
                {
                    new Claim(Constants.ClaimType, bitcoinPublicAddress)
                });
        }
    }
}