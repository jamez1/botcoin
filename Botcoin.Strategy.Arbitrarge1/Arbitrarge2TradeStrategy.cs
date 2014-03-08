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

        public Arbitrarge2TradeStrategy( INotificationEngine _notificationEngine, ICurrencyPairRepository _currencyPairRepository)
        {
            currencyPairRepository = _currencyPairRepository;
            notificationEngine = _notificationEngine;
        }

        private bool Compare(CurrencyWalletPair exchange1, CurrencyWalletPair exchange2)
        {
            var quote1 = exchange1.LastTick();
            var quote2 = exchange2.LastTick();

            if (quote1 == null || quote2 == null)
                return false;

            if (quote1.Bid <= quote2.Ask)
                return false;

            #region -- Prepare --

            string desc = string.Format("Arb detected, buy {1} sell {0}", quote1.SourceExchange, quote2.SourceExchange);
            Console.WriteLine(desc);

            decimal qty = 1; //Determine this based on how much cash is left

            #endregion 



            #region -- Execution --

            var trade = new Trade(desc);

            /*
            trade.Transactions.Add(new MarketOrderBuyBTCTransaction(exchange1, qty, quote1.Bid));
            trade.Transactions.Add(new MarketOrderSellBTCTransaction(exchange2, qty, quote1.Bid));
            */
            //notificationEngine.TradeSignal(quote1, quote2);
            notificationEngine.TradeSignal(trade);

            #endregion

            return true;
        }

        public void Execute()
        {
            //Clever loop to compare each exchange against eachother once

            var pairs = currencyPairRepository.GetAll().Where(c => c.sourceWallet.Currency == CurrencyType.Bitcoin
                && c.destinationWallet.Currency == CurrencyType.Litecoin).ToArray();

            int exchangeCount = pairs.Count();

            for (int i = 0; i < exchangeCount; i++)
            {
                for (int j = i + 1; j < exchangeCount; j++)
                {

                    //Try buy the first, sell the second
                    var result = Compare(pairs[i], pairs[j]);

                    //If that didn't work try buy the second sell the first
                    if (!result)
                        Compare(pairs[j], pairs[i]);
                    
                }
            }
        }
    }
}
