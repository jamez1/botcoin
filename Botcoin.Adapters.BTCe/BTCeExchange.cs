using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Botcoin.Shared;
using Newtonsoft.Json;

namespace Botcoin.Adapters.BTCe
{
    public class BTCeExchange: IExchange
    {
        readonly IDataStore dataStore;
        private TickDataModel lastQuote;
        private string getUrl = "https://btc-e.com/api/2/btc_usd/ticker";
        private string sourceExchange = "BTCe";

        public string friendlyName()
        {
            return sourceExchange;
        }

        public BTCeExchange(IDataStore _dataStore)
        {
            dataStore = _dataStore;
        }

        public void UpdateQuotes()
        {
            WebClient client = new WebClient(); 
            var source = client.DownloadString(getUrl);

            var sourceTickerContainer = JsonConvert.DeserializeObject<BTCeExchangeTickerContainerModel>(source);

            var sourceTicker = sourceTickerContainer.ticker;

            var tick = new TickDataModel();

            tick.SourceExchange = sourceExchange;
            //tick.Average= sourceTicker.av
            //tick.Volum;
            tick.High = sourceTicker.high;
            tick.Low = sourceTicker.low;
            tick.Last   = sourceTicker.last;
            tick.Bid    = sourceTicker.buy;
            tick.Ask = sourceTicker.sell;

            dataStore.Save(tick);
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
