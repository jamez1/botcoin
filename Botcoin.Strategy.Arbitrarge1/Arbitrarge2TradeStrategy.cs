using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;
using Botcoin.Shared.Enums;
using Botcoin.Shared.Interfaces;
using Botcoin.Shared.Models;

namespace Botcoin.Strategy.Arbitrarge1
{
    public class Arbitrarge2TradeStrategy: ITradeStrategy
    {
        readonly ICurrencyPairRepository currencyPairRepository;
        readonly INotificationEngine notificationEngine;
        readonly IDataStore dataStore;

        public Arbitrarge2TradeStrategy( INotificationEngine _notificationEngine, ICurrencyPairRepository _currencyPairRepository, IDataStore _dataStore)
        {
            currencyPairRepository = _currencyPairRepository;
            notificationEngine = _notificationEngine;
            dataStore = _dataStore;
        }

        private bool Compare(CurrencyWalletPair exchange1, CurrencyWalletPair exchange2)
        {
            var quote1 = exchange1.LastTick();
            var quote2 = exchange2.LastTick();

            if (quote1 == null || quote2 == null)
                return false;

            string desc = string.Format("Arb detected, buy {1} sell {0}", quote1.SourceExchange, quote2.SourceExchange);

            CurrencyWalletPair ltcRebalance = new CurrencyWalletPair(exchange1.destinationWallet, exchange2.destinationWallet
                , dataStore);
            CurrencyWalletPair btcRebalance = new CurrencyWalletPair(exchange1.sourceWallet, exchange2.sourceWallet
                , dataStore);

            #region -- Execution --

            var trade = new Trade(desc);


            trade.Transactions.Add(new Transaction(exchange1));
            trade.Transactions.Add(new Transaction(exchange2));
            trade.Transactions.Add(new Transaction(ltcRebalance));
            trade.Transactions.Add(new Transaction(btcRebalance));

            var result = trade.Execute(1);

            if (result <= 1)
                return false; //Loses money


            //notificationEngine.TradeSignal(quote1, quote2);
            notificationEngine.TradeSignal(trade);

            #endregion

            return true;
        }

        public void Execute()
        {
            //Clever loop to compare each exchange against eachother once
            var pairs = currencyPairRepository.GetAll();

            var matchingPairs = pairs.Where(c => c.sourceWallet.Currency == CurrencyType.Bitcoin
                && c.destinationWallet.Currency == CurrencyType.Litecoin).ToArray();

            int exchangeCount = matchingPairs.Count();

            for (int i = 0; i < exchangeCount; i++)
            {
                for (int j = i + 1; j < exchangeCount; j++)
                {

                    //Try buy the first, sell the second
                    var result = Compare(matchingPairs[i], matchingPairs[j]);

                    //If that didn't work try buy the second sell the first
                    if (!result)
                        Compare(matchingPairs[j], matchingPairs[i]);
                    
                }
            }
        }
    }
}
