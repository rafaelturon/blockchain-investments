using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blockchain.Investments.Core.Model
{
    public class Asset : IEntity
    {
        private string id = string.Empty;

        public string Name {get; set;}
        public double Value {get; set;}
        public DateTime Date {get; set;}
        public int Percentage {get;set;}
        public string ImageSource {get;set;}
        public string UniqueId
        {
            get
            {
                if (!this.Id.ToString().Equals("000000000000000000000000"))
                    id = this.Id.ToString();

                return id;
            }
            set
            {
                id = value;
            }
        }
    }
}
