using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Botcoin.Shared;
using Newtonsoft.Json;

namespace Botcoin.Adapters.Bitstamp
{
    public class BitStampExchange: IExchange
    {
        readonly IDataStore dataStore;
        private TickDataModel lastQuote;
        private string getUrl = "https://www.bitstamp.net/api/ticker/";
        private string sourceExchange = "BitStamp";

        public string friendlyName()
        {
                return sourceExchange;
        }
        public BitStampExchange(IDataStore _dataStore)
        {
            dataStore = _dataStore;
        }

        public void UpdateQuotes()
        {
            WebClient client = new WebClient(); 
            var source = client.DownloadString(getUrl);

            var sourceTicker = JsonConvert.DeserializeObject<BitStampTickerModel>(source);

            var tick = new TickDataModel();

            tick.SourceExchange = sourceExchange;
            tick.High   = sourceTicker.high;
            tick.Low    = sourceTicker.low;
            //tick.Average= sourceTicker.av
            //tick.Volum;
            tick.Last   = sourceTicker.last;
            tick.Bid    = sourceTicker.bid;
            tick.Ask = sourceTicker.ask;

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
