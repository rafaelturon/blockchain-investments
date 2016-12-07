using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentsApi.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion;
        }
    }
}