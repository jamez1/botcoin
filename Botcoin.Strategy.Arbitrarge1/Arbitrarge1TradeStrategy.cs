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
            //TODO: Implement real algorithm
            if (quote1.Bid > quote2.Ask)
                Console.WriteLine("Arb detected");

            if (quote2.Bid > quote1.Ask)
                Console.WriteLine("Arb detected");
        }

        public void Execute()
        {
            //TODO: Be bothered to write the proper loop, this is so terrible it's not funny
            //As a bonus as I've only implemented one exchange it actually compares that exchange against itself
            //so I can test haha!

            foreach (var exchange1 in exchanges)
            {
                foreach (var exchange2 in exchanges)
                {
                    var quote1 = exchange1.GetLastQuote();
                    var quote2 = exchange2.GetLastQuote();
                }
            }
        }
    }
}
