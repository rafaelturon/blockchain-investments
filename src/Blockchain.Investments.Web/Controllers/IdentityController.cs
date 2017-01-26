using System;
using Blockchain.Investments.Bitcoin.Domain;
using Blockchain.Investments.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        [HttpGet]
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

            return new ObjectResult(bitIdRequest);
        }

        [HttpPost]
        public IActionResult Post([FromBody]BitIdCredentials request) 
        {
            BitIdResponse response = request.VerifyMessage();

            return new ObjectResult(response);
        }
    }
}