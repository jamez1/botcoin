using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Botcoin.Adapters.BTCe;
using Botcoin.Shared;
using Botcoin.Shared.Enums;
using Botcoin.Shared.Interfaces;
using Botcoin.Shared.Models;
using Newtonsoft.Json;

namespace Botcoin.Adapters.BTCMarkets
{
    public class BTCMarketsExchange: IExchange
    {
        List<CurrencyWallet> wallets;
        List<BTCMarketsCurrencyWalletPair> pairsToUpdate;
        List<BTCMarketsSyntheticCurrencyWalletPair> syntheticPairsToUpdate;

        readonly IDataStore dataStore;
        private TickDataModel lastQuote;
        private string getUrl = "https://api.btcmarkets.net/market/BTC/AUD/tick";
        private string sourceExchange = "BTCMarkets";

        public string friendlyName()
        {
            return sourceExchange;
        }

        public BTCMarketsExchange(IDataStore _dataStore)
        {
            dataStore = _dataStore;
        }

        public BTCMarketsExchange(IDataStore _dataStore, ICurrencyPairRepository _currencyPairRepository)
        {
            wallets = new List<CurrencyWallet>();
            pairsToUpdate = new List<BTCMarketsCurrencyWalletPair>();
            syntheticPairsToUpdate = new List<BTCMarketsSyntheticCurrencyWalletPair>();

            dataStore = _dataStore;
            var btcWallet = new FixedFeeCurrencyWallet(0.001M,0.001M, CurrencyType.Bitcoin , this);
            var ltcWallet = new FixedFeeCurrencyWallet(0.01M, 0.01M, CurrencyType.Litecoin, this);
            var audWallet = new PercentageFeeCurrencyWallet(0.01M, 0.015M, CurrencyType.AUD, this);

            wallets.Add(btcWallet);
            wallets.Add(ltcWallet);
            wallets.Add(audWallet);

            var btcusd = new BTCMarketsCurrencyWalletPair(btcWallet, audWallet, _dataStore, "https://api.btcmarkets.net/market/BTC/AUD/tick");
            var ltcusd = new BTCMarketsCurrencyWalletPair(ltcWallet, audWallet, _dataStore, "https://api.btcmarkets.net/market/LTC/AUD/tick");
            var btcltc = new BTCMarketsSyntheticCurrencyWalletPair(btcWallet, ltcWallet, _dataStore, "https://api.btcmarkets.net/market/BTC/AUD/tick", "https://api.btcmarkets.net/market/LTC/AUD/tick");

            pairsToUpdate.Add(btcusd);
            pairsToUpdate.Add(ltcusd);
            syntheticPairsToUpdate.Add(btcltc);

            _currencyPairRepository.Store(btcusd);
            _currencyPairRepository.Store(ltcusd);
            _currencyPairRepository.Store(btcltc);
        }


        public void UpdateQuotes()
        {
            foreach (var pair in pairsToUpdate)
                UpdateQuotes(pair);

            foreach (var syntheticPair in syntheticPairsToUpdate)
                UpdateQuotes(syntheticPair);

            Thread.Sleep(1000);
        }


        private void UpdateQuotes(BTCMarketsSyntheticCurrencyWalletPair pair)
        {
            WebClient client = new WebClient();
            var source1 = client.DownloadString(pair.url);
            var source2 = client.DownloadString(pair.url2);

            var sourceTicker = JsonConvert.DeserializeObject<BTCMarketsTickerModel>(source1);
            var sourceTicker2 = JsonConvert.DeserializeObject<BTCMarketsTickerModel>(source2);

            var tick = new TickDataModel();

            tick.SourceExchange = sourceExchange + " Synthetic BTC/LTC";
            //tick.Average= sourceTicker.av
            //tick.Volum;
            tick.Last = sourceTicker.lastPrice / sourceTicker2.lastPrice;
            tick.Bid = sourceTicker.bestBid / sourceTicker2.bestAsk;
            tick.Ask = sourceTicker.bestAsk / sourceTicker2.bestBid;


            pair.Tick(tick);
            lastQuote = tick;

            return;
        }

        private void UpdateQuotes(BTCMarketsCurrencyWalletPair pair)
        {
            WebClient client = new WebClient();
            var source = client.DownloadString(pair.url);

            var sourceTicker = JsonConvert.DeserializeObject<BTCMarketsTickerModel>(source);

            var tick = new TickDataModel();

            tick.SourceExchange = sourceExchange;
            //tick.Average= sourceTicker.av
            //tick.Volum;
            tick.Last = sourceTicker.lastPrice;
            tick.Bid = sourceTicker.bestBid;
            tick.Ask = sourceTicker.bestAsk;


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
