using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;
using Botcoin.Shared.Models;

namespace Botcoin.Adapters.BTCe
{
    public class BTCMarketsCurrencyWalletPair : CurrencyWalletPair
    {
        public string url;

        public BTCMarketsCurrencyWalletPair(CurrencyWallet _sourceWallet, CurrencyWallet _destinationWallet, IDataStore _dataStore, string _url)
            : base(_sourceWallet, _destinationWallet, _dataStore)
        {
            url = _url;
        }
    }
}
