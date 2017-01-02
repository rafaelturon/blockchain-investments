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
    public class CurrencyController : Controller
    {
        private readonly ILogger<CurrencyController> _logger;
        private IRepository _repo;
        private readonly AppConfig _optionsAccessor;

        public CurrencyController (ILogger<CurrencyController> logger, IRepository repo, IOptions<AppConfig> optionsAccessor)
        {
            _logger = logger;
            _repo = repo;
            _optionsAccessor = optionsAccessor.Value;

            string conn = _optionsAccessor.MONGOLAB_URI;
            _repo.Initialize("Currency");
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Currency> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _repo.FindAll<Currency>();
        }

        // GET api/values/5
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);
            
            var currency = _repo.FindById<Currency>(id);
            if (currency == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(currency);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Currency currency)
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
        public IActionResult Put([FromBody]Currency currency)
        {
            if (currency == null || string.IsNullOrEmpty(currency.UniqueId))
            {
                return BadRequest();
            }

            var currentCurrency = _repo.FindById<Currency>(currency.UniqueId);
            if (currentCurrency == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Update({0}) NOT FOUND", currency.UniqueId);
                return NotFound();
            }
            
            _repo.Update(currency.UniqueId, currency);
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Updated", currency.UniqueId);
            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var currency = _repo.FindById<Currency>(id);
            if (currency == null)
            {
                return NotFound();
            }
 
            _repo.Remove<Currency>(id);
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Deleted", id);
            return new OkResult();
        }
    }
}
