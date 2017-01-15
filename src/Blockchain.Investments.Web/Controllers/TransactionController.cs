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
        // public IEnumerable<TransactionItemListDto> Get()
        // {
        //     _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
        //     return _readmodel.GetTransactionItems();
        // }
        public JournalEntry Get()
        {
            JournalEntry entry = new JournalEntry();
            
            // Currency
            entry.Currency = new Security();
            entry.Currency.Code = "986";
            entry.Currency.Description = "The Brazilian Real is the currency of Brazil";
            entry.Currency.DetailsUrl = "http://www.xe.com/currency/brl-brazilian-real";
            entry.Currency.Fraction = 100;
            entry.Currency.ImageUrl = "http://www.xe.com/themes/xe/images/flags/big/brl.png";
            entry.Currency.Namespace = "Forex";
            entry.Currency.Pricing = PricingMechanism.Market;
            entry.Currency.Type = MarketType.ForexCurrency;
            entry.Currency.QuoteSource = "http://www.xe.com";
            entry.Currency.Ticker = "BRL";
            entry.Currency.Title = "Real";

            // Description
            entry.Description = "Initial Balance";

            // EventDate
            entry.EventDate = DateTime.Now;

            // Splits
            // > Transaction#1
            Transaction tx1 = new Transaction();
            tx1.Journal = JournalType.Credit;
            tx1.Quantity = 1;
            tx1.Value = 10000;
            tx1.Tag = "Deposit";
            tx1.Account = new Account();
            tx1.Account.CounterpartyType = CounterpartyType.Bank;
            tx1.Account.Security = new Security();
            tx1.Account.Security.Code = "986";
            tx1.Account.Security.Description = "The Brazilian Real is the currency of Brazil";
            tx1.Account.Security.DetailsUrl = "http://www.xe.com/currency/brl-brazilian-real";
            tx1.Account.Security.Fraction = 100;
            tx1.Account.Security.ImageUrl = "http://www.xe.com/themes/xe/images/flags/big/brl.png";
            tx1.Account.Security.Namespace = "Forex";
            tx1.Account.Security.Pricing = PricingMechanism.Market;
            tx1.Account.Security.Type = MarketType.ForexCurrency;
            tx1.Account.Security.QuoteSource = "http://www.xe.com";
            tx1.Account.Security.Ticker = "BRL";
            tx1.Account.Security.Title = "Real";

            // > Transaction#2
            Transaction tx2 = new Transaction();
            tx2.Journal = JournalType.Debit;
            tx2.Quantity = 1;
            tx2.Value = 10000;
            tx2.Tag = "Equity";
            tx2.Account = new Account();
            tx2.Account.CounterpartyType = CounterpartyType.Bank;
            tx2.Account.Security = new Security();
            tx2.Account.Security.Code = "986";
            tx2.Account.Security.Description = "The Brazilian Real is the currency of Brazil";
            tx2.Account.Security.DetailsUrl = "http://www.xe.com/currency/brl-brazilian-real";
            tx2.Account.Security.Fraction = 100;
            tx2.Account.Security.ImageUrl = "http://www.xe.com/themes/xe/images/flags/big/brl.png";
            tx2.Account.Security.Namespace = "Forex";
            tx2.Account.Security.Pricing = PricingMechanism.Market;
            tx2.Account.Security.Type = MarketType.ForexCurrency;
            tx2.Account.Security.QuoteSource = "http://www.xe.com";
            tx2.Account.Security.Ticker = "BRL";
            tx2.Account.Security.Title = "Real";

            entry.Splits = new List<Transaction>();
            entry.Splits.Add(tx1);
            entry.Splits.Add(tx2);

            return entry;
            //_logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            //return _readmodel.GetTransactionItems();
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
        [HttpPost]
        public IActionResult Post([FromBody]JournalEntry journalEntry)
        {
            string userId = "12345";
            if (journalEntry == null || journalEntry.EventDate == null ||
                 journalEntry.Splits == null || journalEntry.Splits.Count < 2)
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