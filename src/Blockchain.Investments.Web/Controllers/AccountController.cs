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
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AppConfig _optionsAccessor;
        private IRepository _repo;

        public AccountController (ILogger<AccountController> logger, IRepository repo, IOptions<AppConfig> optionsAccessor)
        {
            _logger = logger;
            _optionsAccessor = optionsAccessor.Value;
            _repo = repo;
            
            string conn = _optionsAccessor.MONGOLAB_URI;
            _repo.Initialize(conn, Constants.DatabaseName, "Account");
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _repo.FindAll<Account>();
        }

        // GET api/values/5
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);
            
            var account = _repo.FindById<Account>(id);
            if (account == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(account);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Account account)
        {
            if (account == null)
            {
                return BadRequest();
            }
            var createdAccount = _repo.Create(account);
            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Created", createdAccount.UniqueId);
            return new OkObjectResult(account);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Account account)
        {
            if (account == null || string.IsNullOrEmpty(account.UniqueId))
            {
                return BadRequest();
            }

            var currentAccount = _repo.FindById<Account>(account.UniqueId);
            if (currentAccount == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Update({0}) NOT FOUND", account.UniqueId);
                return NotFound();
            }
            
            _repo.Update(account.UniqueId, account);
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Updated", account.UniqueId);
            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var account = _repo.FindById<Account>(id);
            if (account == null)
            {
                return NotFound();
            }
 
            _repo.Remove<Account>(id);
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Deleted", id);
            return new OkResult();
        }
    }
}
