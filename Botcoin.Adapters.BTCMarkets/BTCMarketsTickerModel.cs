using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Adapters.BTCMarkets
{
    //Results from https://api.btcmarkets.net/market/BTC/AUD/tick
    class BTCMarketsTickerModel
    {
        public decimal lastPrice { get; set; }
        public decimal bestBid { get; set; }
        public decimal volume { get; set; }
        public int timestamp { get; set; }
        public decimal bestAsk { get; set; }
        public string currency { get; set; }
        public string instrument { get; set; }
    }
}
