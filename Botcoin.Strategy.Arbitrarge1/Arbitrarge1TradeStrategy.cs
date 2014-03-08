using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;
using Botcoin.Shared.Models;

namespace Botcoin.Strategy.Arbitrarge1
{
    [Obsolete]
    public class Arbitrarge1TradeStrategy: ITradeStrategy
    {
        readonly IExchange[] exchanges;
        readonly INotificationEngine notificationEngine;

        public Arbitrarge1TradeStrategy(IExchange[] _exchanges, INotificationEngine _notificationEngine)
        {
            exchanges = _exchanges;
            notificationEngine = _notificationEngine;
        }

        private bool Compare(IExchange exchange1, IExchange exchange2)
        {
            var quote1 = exchange1.GetLastQuote();
            var quote2 = exchange2.GetLastQuote();

            if (quote1 == null || quote2 == null)
                return false;

            if (quote1.Bid <= quote2.Ask)
                return false;

            #region -- Prepare --

            string desc = string.Format("Arb detected, buy {1} sell {0}", quote1.SourceExchange, quote2.SourceExchange);
            Console.WriteLine(desc);

            decimal qty = 1; //Determine this based on how much cash is left

            #endregion 



            #region -- Execution --

            var trade = new Trade(desc);

            /*
            trade.Transactions.Add(new MarketOrderBuyBTCTransaction(exchange1, qty, quote1.Bid));
            trade.Transactions.Add(new MarketOrderSellBTCTransaction(exchange2, qty, quote1.Bid));
            */
            //notificationEngine.TradeSignal(quote1, quote2);
            notificationEngine.TradeSignal(trade);

            #endregion

            return true;
        }

        public void Execute()
        {
            //Clever loop to compare each exchange against eachother once

            int exchangeCount = exchanges.Count();

            for (int i = 0; i < exchangeCount; i++)
            {
                for (int j = i + 1; j < exchangeCount; j++)
                {

                    //Try buy the first, sell the second
                    var result = Compare(exchanges[i], exchanges[j]);

                    //If that didn't work try buy the second sell the first
                    if (!result)
                        Compare(exchanges[j], exchanges[i]);
                    
                }
            }
        }
    }
}
