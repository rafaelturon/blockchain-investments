using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blockchain.Investments.Core;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Control;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class AssetsController : Controller
    {
        private readonly ILogger<AssetsController> _logger;
        private AssetRepo _repo;

        public AssetsController (ILogger<AssetsController> logger, AssetRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Asset> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            AssetControl assetControl = new AssetControl();

            return assetControl.List();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);
            Asset asset = new Asset();
            asset.Name = "test";
            return asset.Name;
        }

        // POST api/values
        [HttpPost]
        public IActionResult  Post([FromBody]Asset asset)
        {
            _repo.Create(asset);
            return new OkObjectResult(asset);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
