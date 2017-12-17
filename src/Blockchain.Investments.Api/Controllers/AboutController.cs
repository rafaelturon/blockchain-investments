using Microsoft.AspNetCore.Authorization;
using Blockchain.Investments.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AboutController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion;
        }

        [HttpGet]
        [Route("echo")]
        public string Echo(string message) 
        {
            return message;
        }

        [HttpGet]
        [Route("getid")]
        public string GetId() 
        {
            return Util.NewSequentialId().ToString();
        }
    }
}