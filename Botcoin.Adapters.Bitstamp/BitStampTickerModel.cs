using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Adapters.Bitstamp
{
    //JSON De-serialization result from
    //https://www.bitstamp.net/api/ticker/
    public class BitStampTickerModel
    {
        public decimal high      {get;set;}
        public decimal last      {get;set;}
        public decimal bid       {get;set;}
        public decimal volume    {get;set;}
        public int timestamp {get;set;}
        public decimal low       {get;set;}
        public decimal ask { get; set; }
    }
}
