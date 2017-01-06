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
    public class MarketController : Controller
    {
        private readonly ILogger<MarketController> _logger;
        private IRepository<Market> _repo;
        private readonly AppConfig _optionsAccessor;

        public MarketController (ILogger<MarketController> logger, IRepository<Market> repo, IOptions<AppConfig> optionsAccessor)
        {
            _logger = logger;
            _repo = repo;
            _optionsAccessor = optionsAccessor.Value;

            string conn = _optionsAccessor.MONGOLAB_URI;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Market> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _repo.FindAll();
        }

        // GET api/values/5
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);
            
            var currency = _repo.FindById(id);
            if (currency == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(currency);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Market currency)
        {
            if (currency == null)
            {
                return BadRequest();
            }
            var createdCurrency = _repo.Create(currency);
            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Created", createdCurrency.UniqueId);
            return new OkObjectResult(currency);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Market market)
        {
            if (market == null || string.IsNullOrEmpty(market.UniqueId))
            {
                return BadRequest();
            }

            var currentMarket = _repo.FindById(market.UniqueId);
            if (currentMarket == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Update({0}) NOT FOUND", market.UniqueId);
                return NotFound();
            }
            
            _repo.Update(market.UniqueId, market);
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Updated", market.UniqueId);
            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var currency = _repo.FindById(id);
            if (currency == null)
            {
                return NotFound();
            }
 
            _repo.Remove(id);
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Deleted", id);
            return new OkResult();
        }
    }
}
