using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Shared.Models
{
    public class MarketOrderBuyBTCTransaction : Transaction
    {
        readonly IExchange exchange;
        readonly decimal qty;

        public MarketOrderBuyBTCTransaction(IExchange _exchange, decimal _qty, decimal priceTarget)
        {
            exchange = _exchange;
            qty = _qty;
        }

        public string toString()
        {
            return "Buy BTC Order on " + exchange.friendlyName() + " for " + qty + " BTC";
        }
    }
}
