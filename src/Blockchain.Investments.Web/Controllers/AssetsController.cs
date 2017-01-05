using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft​.Extensions​.Options;
using Blockchain.Investments.Core;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class AssetsController : Controller
    {
        private readonly ILogger<AssetsController> _logger;
        private IRepository<Asset> _repo;
        private readonly AppConfig _optionsAccessor;

        public AssetsController (ILogger<AssetsController> logger, IRepository<Asset> repo, IOptions<AppConfig> optionsAccessor)
        {
            _logger = logger;
            _repo = repo;
            _optionsAccessor = optionsAccessor.Value;
            
            string conn = _optionsAccessor.MONGOLAB_URI;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Asset> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _repo.FindAll();
        }

        // GET api/values/5
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);
            
            var asset = _repo.FindById(id);
            if (asset == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(asset);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Asset asset)
        {
            if (asset == null)
            {
                return BadRequest();
            }
            var createdAsset = _repo.Create(asset);
            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Created", createdAsset.UniqueId);
            return new OkObjectResult(asset);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Asset asset)
        {
            if (asset == null || string.IsNullOrEmpty(asset.UniqueId))
            {
                return BadRequest();
            }

            var currentAsset = _repo.FindById(asset.UniqueId);
            if (currentAsset == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Update({0}) NOT FOUND", asset.UniqueId);
                return NotFound();
            }
            
            _repo.Update(asset.UniqueId, asset);
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Updated", asset.UniqueId);
            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var asset = _repo.FindById(id);
            if (asset == null)
            {
                return NotFound();
            }
 
            _repo.Remove(id);
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Deleted", id);
            return new OkResult();
        }
    }
}
