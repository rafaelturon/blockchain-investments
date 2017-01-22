using System;

namespace Blockchain.Investments.Core.Domain
{
    public class Period : BaseEntity
    {
        public string Title {get; set;}
        public int Days {get;set;}
        public int Terms {get;set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
    }
}
