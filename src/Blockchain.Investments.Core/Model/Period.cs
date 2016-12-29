using System;

namespace Blockchain.Investments.Core.Model
{
    public class Period : IEntity
    {
        private string id = string.Empty;
        public string Title {get; set;}
        public int Days {get;set;}
        public int Terms {get;set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
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
