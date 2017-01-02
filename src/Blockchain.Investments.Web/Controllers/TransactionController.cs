using System;
using System.Linq;
using System.Collections.Generic;
using Blockchain.Investments.Core;
using Blockchain.Investments.Core.ReadModel;
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.Repositories;
using Blockchain.Investments.Core.WriteModel.Commands;
using CQRSlite.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly AppConfig _optionsAccessor;
        private readonly IRepository _repo;
        private readonly ICommandSender _commandSender;
        private readonly IReadModelFacade _readmodel;

        public TransactionController (ILogger<TransactionController> logger,
                                        IRepository repo,
                                        IOptions<AppConfig> optionsAccessor,
                                        ICommandSender commandSender,
                                        IReadModelFacade readmodel)
        {
            _logger = logger;
            _optionsAccessor = optionsAccessor.Value;
            _repo = repo;
            _readmodel = readmodel;
            _commandSender = commandSender;
            
            string conn = _optionsAccessor.MONGOLAB_URI;
            _repo.Initialize("Ledger");

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<TransactionItemListDto> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _readmodel.GetTransactionItems();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id.ToString());
            
            var transaction = _readmodel.GetTransactionItems().FirstOrDefault(p => p.Id == id);
            if (transaction == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id.ToString());
                return NotFound();
            }
            return new ObjectResult(transaction);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Blockchain.Investments.Core.WriteModel.Domain.Transaction transaction)
        {
            if (transaction == null || transaction.Description == null)
            {
                return BadRequest();
            }
            _commandSender.Send(new AddTransaction(Guid.NewGuid(), transaction.Description));
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Added", transaction.Id);
            return new OkResult();
        }
    }
}