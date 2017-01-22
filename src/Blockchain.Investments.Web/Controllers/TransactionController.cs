using System;
using System.Linq;
using System.Collections.Generic;
using Blockchain.Investments.Core;
using Blockchain.Investments.Core.ReadModel;
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.WriteModel.Commands;
using CQRSlite.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Blockchain.Investments.Core.Domain;
using Blockchain.Investments.Core.Infrastructure;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ICommandSender _commandSender;
        private readonly IReadModelFacade _readmodel;

        public TransactionController (ILogger<TransactionController> logger,
                                        ICommandSender commandSender,
                                        IReadModelFacade readmodel)
        {
            _logger = logger;
            _readmodel = readmodel;
            _commandSender = commandSender;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<BookDto> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _readmodel.GetTransactionItems();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id.ToString());
            
            var transaction = _readmodel.GetTransactionItems().FirstOrDefault(p => p.AggregateId == id);
            if (transaction == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id.ToString());
                return NotFound();
            }
            return new ObjectResult(transaction);
        }

        // POST api/values
        [HttpPut]
        public IActionResult Put([FromBody]JournalEntry journalEntry)
        {
            string userId = "12345";
            if (journalEntry == null || journalEntry.EventDate == null ||
                 journalEntry.Splits == null || journalEntry.Splits.Count < 2 ||
                 journalEntry.Version != 0)
            {
                return BadRequest();
            }
            //Guid journalEntryId = Util.NewSequentialId();
            Guid journalEntryId = new Guid("41c0756d-2c91-93c5-64c0-a08d590160b4");
            _commandSender.Send(new CreateJournal(journalEntryId, userId, journalEntry));
            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Added", journalEntryId);
            return new OkResult();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]JournalEntry journalEntry)
        {
            string userId = "12345";
            if (journalEntry == null || journalEntry.EventDate == null ||
                 journalEntry.Splits == null || journalEntry.Splits.Count < 2 ||
                 journalEntry.Version == 0)
            {
                return BadRequest();
            }
            //Guid journalEntryId = Util.NewSequentialId();
            Guid journalEntryId = new Guid("41c0756d-2c91-93c5-64c0-a08d590160b4");
            _commandSender.Send(new AddJournalEntry(journalEntryId, userId, journalEntry));
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Added", journalEntryId);
            return new OkResult();
        }
    }
}