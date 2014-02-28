using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Botcoin.Shared;
using Newtonsoft.Json;

namespace Botcoin.Adapters.CampBXExchange
{
    public class CampBXExchange: IExchange
    {
        readonly IDataStore dataStore;
        private TickDataModel lastQuote;
        private string getUrl = "http://campbx.com/api/xticker.php";
        private string sourceExchange = "CampBX";

        public string friendlyName()
        {
            return sourceExchange;
        }

        public CampBXExchange(IDataStore _dataStore)
        {
            dataStore = _dataStore;
        }

        public void UpdateQuotes()
        {
            WebClient client = new WebClient(); 
            var source = client.DownloadString(getUrl);

            var sourceTicker = JsonConvert.DeserializeObject<CampBXTickerModel>(source);

            var tick = new TickDataModel();

            tick.SourceExchange = sourceExchange;
            //tick.Average= sourceTicker.av
            //tick.Volum;
            tick.Last   = sourceTicker.Last_Trade;
            tick.Bid    = sourceTicker.Best_Bid;
            tick.Ask = sourceTicker.Best_Ask;

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
