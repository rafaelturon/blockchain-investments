using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Control;

namespace InvestmentsApi.Controllers
{
    [Route("api/[controller]")]
    public class AssetsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Asset> Get()
        {
            AssetControl assetControl = new AssetControl();

            return assetControl.List();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Asset asset = new Asset();
            asset.Name = "test";
            return asset.Name;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
