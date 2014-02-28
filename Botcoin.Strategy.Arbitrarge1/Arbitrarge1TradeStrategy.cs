using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;

namespace Botcoin.Strategy.Arbitrarge1
{
    public class Arbitrarge1TradeStrategy: ITradeStrategy
    {
        readonly IExchange[] exchanges;
        public Arbitrarge1TradeStrategy(IExchange[] _exchanges)
        {
            exchanges = _exchanges;
        }

        private void Compare(TickDataModel quote1, TickDataModel quote2)
        {
            //This algorithm will do

            if (quote1.Bid > quote2.Ask)
                Console.WriteLine("Arb detected, buy {1} sell {0}", quote1.SourceExchange, quote2.SourceExchange);

            else if (quote2.Bid > quote1.Ask)
                Console.WriteLine("Arb detected, buy {1} sell {0}", quote2.SourceExchange, quote1.SourceExchange);
        }

        public void Execute()
        {
            //Clever loop to compare each exchange against eachother once

            int exchangeCount = exchanges.Count();

            for (int i = 0; i < exchangeCount; i++)
            {
                for (int j = i + 1; j < exchangeCount; j++)
                {
                    var quote1 = exchanges[i].GetLastQuote();
                    var quote2 = exchanges[j].GetLastQuote();

                    if (quote1 != null && quote2 !=null)
                        Compare(quote1, quote2);
                }
            }
        }
    }
}
