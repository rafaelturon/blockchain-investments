using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockchain.Investments.Core.Model
{
    public class Asset
    {
        public string Name {get; set;}
        public double Value {get; set;}
        public DateTime Date {get; set;}
        public int Percentage {get;set;}
        public string ImageSource {get;set;}
    }
}
