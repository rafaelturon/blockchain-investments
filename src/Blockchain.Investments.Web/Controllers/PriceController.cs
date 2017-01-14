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
    public class PriceController : Controller
    {
        private readonly ILogger<PriceController> _logger;
        private IRepository<Price> _repo;
        private readonly AppConfig _optionsAccessor;

        public PriceController (ILogger<PriceController> logger, IRepository<Price> repo, IOptions<AppConfig> optionsAccessor)
        {
            _logger = logger;
            _repo = repo;
            _optionsAccessor = optionsAccessor.Value;

            string conn = _optionsAccessor.MONGOLAB_URI;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Price> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _repo.FindAll();
        }

        // GET api/values/5
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);
            
            var price = _repo.FindById(id);
            if (price == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(price);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Price price)
        {
            if (price == null)
            {
                return BadRequest();
            }
            var newPrice = _repo.Create(price);
            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Created", newPrice.UniqueId);
            return new OkObjectResult(price);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Price price)
        {
            if (price == null || string.IsNullOrEmpty(price.UniqueId))
            {
                return BadRequest();
            }

            var currentPrice = _repo.FindById(price.UniqueId);
            if (currentPrice == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Update({0}) NOT FOUND", price.UniqueId);
                return NotFound();
            }
            
            _repo.Update(price.UniqueId, price);
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Updated", price.UniqueId);
            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var price = _repo.FindById(id);
            if (price == null)
            {
                return NotFound();
            }
 
            _repo.Remove(id);
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Deleted", id);
            return new OkResult();
        }
    }
}
