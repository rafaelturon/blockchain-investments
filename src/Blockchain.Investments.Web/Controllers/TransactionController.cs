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
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly AppConfig _optionsAccessor;
        private readonly ICommandSender _commandSender;
        private readonly IReadModelFacade _readmodel;

        public TransactionController (ILogger<TransactionController> logger,
                                        IOptions<AppConfig> optionsAccessor,
                                        ICommandSender commandSender,
                                        IReadModelFacade readmodel)
        {
            _logger = logger;
            _optionsAccessor = optionsAccessor.Value;
            _readmodel = readmodel;
            _commandSender = commandSender;
            
            string conn = _optionsAccessor.MONGOLAB_URI;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<TransactionItemListDto> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _readmodel.GetTransactionItems();

            // List<TransactionItemListDto> dto = new List<TransactionItemListDto>();
            // Guid journalEntryId = Util.NewSequentialId();
            // string userId = "12345";
            // JournalEntry entry = new JournalEntry();
            // entry.EventDate = System.DateTime.Now;
            // entry.CreditTo =  new Transaction();
            // entry.CreditTo.Account = new ChartAccount();
            // entry.CreditTo.Account.AccountType = AccountType.Asset;
            // entry.CreditTo.Amount = 100;
            // entry.CreditTo.Journal = JournalType.Withdrawal;
            // entry.CreditTo.Security = new Security();
            // entry.CreditTo.Security.Country = "BR";
            // entry.CreditTo.Security.Description = "Bradesco";
            // entry.CreditTo.Security.Pricing = PricingMechanism.Historical;
            // entry.CreditTo.Security.Ticker = "BRL";
            // entry.CreditTo.Security.Type = MarketType.ForexCurrency;
            // entry.DebtFrom =  new Transaction();
            // entry.DebtFrom.Account = new ChartAccount();
            // entry.DebtFrom.Account.AccountType = AccountType.Asset;
            // entry.DebtFrom.Amount = 100;
            // entry.DebtFrom.Journal = JournalType.Deposit;
            // entry.DebtFrom.Security = new Security();
            // entry.DebtFrom.Security.Country = "BR";
            // entry.DebtFrom.Security.Description = "Santander";
            // entry.DebtFrom.Security.Pricing = PricingMechanism.Historical;
            // entry.DebtFrom.Security.Ticker = "BRL";
            // entry.DebtFrom.Security.Type = MarketType.ForexCurrency;
            // TransactionItemListDto item = new TransactionItemListDto(journalEntryId, userId, entry);
            // dto.Add(item);
            // return dto;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id.ToString());
            
            var transaction = _readmodel.GetTransactionItems().FirstOrDefault(p => p.TransactionId == id);
            if (transaction == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id.ToString());
                return NotFound();
            }
            return new ObjectResult(transaction);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]JournalEntry journalEntry)
        {
            string userId = "";
            if (journalEntry == null || journalEntry.EventDate == null ||
                 journalEntry.DebtFrom == null || journalEntry.CreditTo == null)
            {
                return BadRequest();
            }
            Guid journalEntryId = Util.NewSequentialId();
            _commandSender.Send(new AddJournalEntry(journalEntryId, userId, journalEntry));
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Added", journalEntryId);
            return new OkResult();
        }
    }
}