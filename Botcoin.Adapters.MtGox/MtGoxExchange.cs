using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Botcoin.Shared;
using CCXSharp.Interfaces;
using CCXSharp.Models;
using CCXSharp.MtGox.Models;

namespace Botcoin.Adapters.MtGox
{
    public class MtGoxExchange : IExchange
    {
        readonly IDataStore dataStore;
        readonly IMtGoxExchange mtGoxExchange;
        private TickDataModel lastQuote;

        public MtGoxExchange(IDataStore _dataStore)
        {
            mtGoxExchange = new CCXSharp.MtGox.MtGoxExchange();
            dataStore = _dataStore;
        }

        private decimal Sanitize(string input)
        {
            input = input.Replace('$',' ').Trim();

            return decimal.Parse(input);
        }

        public void UpdateQuotes()
        {
            Ticker sourceTicker = mtGoxExchange.GetTicker(Currency.USD);

            var tick = new TickDataModel();

            tick.High       = Sanitize(sourceTicker.High    );
            tick.Low        = Sanitize(sourceTicker.Low     );
            tick.Average    = Sanitize(sourceTicker.Average );
            //tick.Volume     = Sanitize(sourceTicker.Volume  );
            tick.Last       = Sanitize(sourceTicker.Last    );
            tick.Bid        = Sanitize(sourceTicker.Bid     );
            tick.Ask = Sanitize(sourceTicker.Ask);

            lastQuote = tick;

            Thread.Sleep(1000);
            return;
        }

        public TickDataModel GetLastQuote()
        {
            return lastQuote;
        }
    }
}
