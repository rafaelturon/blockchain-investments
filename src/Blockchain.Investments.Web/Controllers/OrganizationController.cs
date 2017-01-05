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
    public class OrganizationController : Controller
    {
        private readonly ILogger<OrganizationController> _logger;
        private IRepository<Organization> _repo;
        private readonly AppConfig _optionsAccessor;

        public OrganizationController (ILogger<OrganizationController> logger, IRepository<Organization> repo, IOptions<AppConfig> optionsAccessor)
        {
            _logger = logger;
            _repo = repo;
            _optionsAccessor = optionsAccessor.Value;

            string conn = _optionsAccessor.MONGOLAB_URI;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _repo.FindAll();
        }

        // GET api/values/5
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);
            
            var organization = _repo.FindById(id);
            if (organization == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(organization);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Organization organization)
        {
            if (organization == null)
            {
                return BadRequest();
            }
            var createdOrganization = _repo.Create(organization);
            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Created", createdOrganization.UniqueId);
            return new OkObjectResult(organization);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Organization organization)
        {
            if (organization == null || string.IsNullOrEmpty(organization.UniqueId))
            {
                return BadRequest();
            }

            var currentOrganization = _repo.FindById(organization.UniqueId);
            if (currentOrganization == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Update({0}) NOT FOUND", organization.UniqueId);
                return NotFound();
            }
            
            _repo.Update(organization.UniqueId, organization);
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Updated", organization.UniqueId);
            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var organization = _repo.FindById(id);
            if (organization == null)
            {
                return NotFound();
            }
 
            _repo.Remove(id);
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Deleted", id);
            return new OkResult();
        }
    }
}
