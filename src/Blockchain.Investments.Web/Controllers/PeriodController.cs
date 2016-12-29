using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft​.Extensions​.Options;
using Blockchain.Investments.Core;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using Blockchain.Investments.Core.Control;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class PeriodController : Controller
    {
        private readonly ILogger<PeriodController> _logger;
        private PeriodControl _periodControl;
        private IRepository<Period> _repo;
        private readonly AppConfig _optionsAccessor;

        public PeriodController (ILogger<PeriodController> logger, IRepository<Period> repo, IOptions<AppConfig> optionsAccessor)
        {
            _logger = logger;
            _repo = repo;
            _optionsAccessor = optionsAccessor.Value;
            string conn = _optionsAccessor.MONGOLAB_URI;
            _periodControl = new PeriodControl(conn, Constants.DatabaseName);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Period> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _periodControl.List();
        }

        // GET api/values/5
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);
            
            var period = _periodControl.Read(id);
            if (period == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(period);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Period period)
        {
            if (period == null)
            {
                return BadRequest();
            }
            var createdPeriod = _periodControl.Create(period);
            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Created", createdPeriod.UniqueId);
            return new OkObjectResult(period);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Period period)
        {
            if (period == null || string.IsNullOrEmpty(period.UniqueId))
            {
                return BadRequest();
            }

            var currentPeriod = _periodControl.Read(period.UniqueId);
            if (currentPeriod == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Update({0}) NOT FOUND", period.UniqueId);
                return NotFound();
            }
            
            _periodControl.Update(period.UniqueId, period);
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Updated", period.UniqueId);
            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var period = _periodControl.Read(id);
            if (period == null)
            {
                return NotFound();
            }
 
            _periodControl.Delete(period.Id);
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Deleted", id);
            return new OkResult();
        }
    }
}
