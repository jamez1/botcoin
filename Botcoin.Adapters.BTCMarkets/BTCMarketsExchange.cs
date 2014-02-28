using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Botcoin.Shared;
using Newtonsoft.Json;

namespace Botcoin.Adapters.BTCMarkets
{
    public class BTCMarketsExchange: IExchange
    {
        readonly IDataStore dataStore;
        private TickDataModel lastQuote;
        private string getUrl = "https://api.btcmarkets.net/market/BTC/AUD/tick";
        private string sourceExchange = "BTCMarkets";

        public BTCMarketsExchange(IDataStore _dataStore)
        {
            dataStore = _dataStore;
        }

        public void UpdateQuotes()
        {
            WebClient client = new WebClient(); 
            var source = client.DownloadString(getUrl);

            var sourceTicker = JsonConvert.DeserializeObject<BTCMarketsTickerModel>(source);

            var tick = new TickDataModel();

            tick.SourceExchange = sourceExchange;
            //tick.Average= sourceTicker.av
            //tick.Volum;
            tick.Last   = sourceTicker.lastPrice;
            tick.Bid    = sourceTicker.bestBid;
            tick.Ask = sourceTicker.bestAsk;

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
