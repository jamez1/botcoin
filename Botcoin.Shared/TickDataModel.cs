using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Shared
{
    public class TickDataModel
    {
        public string SourceExchange { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Average { get; set; }
        public decimal Volume { get; set; }
        public decimal Last { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
