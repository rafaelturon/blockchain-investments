using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft​.Extensions​.Options;
using Blockchain.Investments.Core.Infrastructure;
using Blockchain.Investments.Core.Domain;
using Blockchain.Investments.Core.Repositories;
using Blockchain.Investments.Api.Requests.Accounts;
using AutoMapper;
using Blockchain.Investments.Core.WriteModel.Commands;
using CQRSlite.Commands;
using Blockchain.Investments.Core.ReadModel.Dtos;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommandSender _commandSender;
        private readonly IRepository<AccountDto> _repo;
        private readonly ILogger<AccountController> _logger;

        public AccountController (ILogger<AccountController> logger, IRepository<AccountDto> repo, ICommandSender commandSender, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _commandSender = commandSender;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<AccountDto> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _repo.FindAll();
        }

        // GET api/values/5
        // [HttpGet("{id:length(24)}")]
        // public IActionResult Get(string id)
        // {
        //     _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);
            
        //     var account = _repo.FindById(id);
        //     if (account == null)
        //     {
        //         _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
        //         return NotFound();
        //     }
        //     return new ObjectResult(account);
        // }

        // POST api/values
        // [HttpPost]
        // public IActionResult Post([FromBody]Account account)
        // {
        //     if (account == null)
        //     {
        //         return BadRequest();
        //     }
        //     var createdAccount = _repo.Create(account);
        //     _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Created", createdAccount.UniqueId);
        //     return new OkObjectResult(account);
        // }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]CreateAccountRequest request)
        {
            var command = _mapper.Map<CreateAccount>(request);
            _commandSender.Send(command);

            return new OkResult();
        }

        // DELETE api/values/5
        // [HttpDelete("{id:length(24)}")]
        // public IActionResult Delete(string id)
        // {
        //     var account = _repo.FindById(id);
        //     if (account == null)
        //     {
        //         return NotFound();
        //     }
 
        //     _repo.Remove(id);
        //     _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Deleted", id);
        //     return new OkResult();
        // }
    }
}
