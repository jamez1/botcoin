using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;
using Botcoin.Shared.Models;

namespace Botcoin.Adapters.BTCe
{
    public class BTCeInverseQuoteCurrencyWalletPair : BTCeCurrencyWalletPair
    {

        public BTCeInverseQuoteCurrencyWalletPair(CurrencyWallet _sourceWallet, CurrencyWallet _destinationWallet, IDataStore _dataStore, string _url)
            : base(_sourceWallet, _destinationWallet, _dataStore, _url)
        {

        }

        public override void Tick(TickDataModel tick)
        {
            lastTick = tick;
            lastTick.Ask = 1 / lastTick.Ask;
            //lastTick.Average = 1 / lastTick.Average;
            lastTick.Bid = 1 / lastTick.Bid;
            lastTick.High = 1 / lastTick.High;
            lastTick.Last = 1 / lastTick.Last;
            lastTick.Low = 1 / lastTick.Low;
            dataStore.Save(tick);//TODO: categorize the data by currencywalletpair
        }


    }
}
