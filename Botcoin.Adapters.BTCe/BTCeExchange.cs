using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Botcoin.Shared;
using Botcoin.Shared.Interfaces;
using Botcoin.Shared.Models;
using Newtonsoft.Json;

namespace Botcoin.Adapters.BTCe
{
    public class BTCeExchange: IExchange
    {
        List<CurrencyWallet> wallets;
        List<BTCeCurrencyWalletPair> pairsToUpdate; 

        readonly IDataStore dataStore;
        private TickDataModel lastQuote;
        private string sourceExchange = "BTCe";

        public string friendlyName()
        {
            return sourceExchange;
        }

        public BTCeExchange(IDataStore _dataStore, ICurrencyPairRepository _currencyPairRepository)
        {
            wallets = new List<CurrencyWallet>();
            pairsToUpdate = new List<BTCeCurrencyWalletPair>();

            dataStore = _dataStore;
            var btcWallet = new FixedFeeCurrencyWallet(0.001M,0.001M, this);
            var ltcWallet = new FixedFeeCurrencyWallet(0.01M,0.01M, this);
            var usdWallet = new PercentageFeeCurrencyWallet(0.01M,0.015M, this);

            wallets.Add(btcWallet);
            wallets.Add(ltcWallet);
            wallets.Add(usdWallet);

            var btcusd = new BTCeCurrencyWalletPair(btcWallet, usdWallet,_dataStore, "https://btc-e.com/api/2/btc_usd/ticker");
            var ltcusd = new BTCeCurrencyWalletPair(ltcWallet, usdWallet,_dataStore, "https://btc-e.com/api/2/ltc_usd/ticker");
            var btcltc = new BTCeCurrencyWalletPair(btcWallet, ltcWallet,_dataStore, "https://btc-e.com/api/2/ltc_btc/ticker");

            pairsToUpdate.Add(btcusd);
            pairsToUpdate.Add(ltcusd);
            pairsToUpdate.Add(btcltc);

            _currencyPairRepository.Store(btcusd);
            _currencyPairRepository.Store(ltcusd);
            _currencyPairRepository.Store(btcltc);
        }


        public void UpdateQuotes()
        {
            foreach (var pair in pairsToUpdate)
                UpdateQuotes(pair);

            Thread.Sleep(1000);
        }

        private void UpdateQuotes(BTCeCurrencyWalletPair pair)
        {
            WebClient client = new WebClient(); 
            var source = client.DownloadString(pair.url);

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

            pair.Tick(tick);
            lastQuote = tick;

            return;
        }

        public TickDataModel GetLastQuote()
        {
            return lastQuote;
        }
    }
}
