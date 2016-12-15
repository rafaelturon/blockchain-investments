using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
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
    }
}