using Microsoft.AspNetCore.Authorization;
using Blockchain.Investments.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    //[AllowAnonymous]
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

        [HttpGet]
        [Route("gettoken")]
        public IActionResult GetToken()
        {
            var dict = new Dictionary<string, string>();

            HttpContext.User.Claims.ToList()
               .ForEach(item => dict.Add(item.Type, item.Value));

            return Ok(dict);
        }
    }
}
