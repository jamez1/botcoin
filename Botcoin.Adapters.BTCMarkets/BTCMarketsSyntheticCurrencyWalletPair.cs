using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;
using Botcoin.Shared.Models;

namespace Botcoin.Adapters.BTCe
{
    public class BTCMarketsSyntheticCurrencyWalletPair : BTCMarketsCurrencyWalletPair
    {
        public string url2;

        public BTCMarketsSyntheticCurrencyWalletPair(CurrencyWallet _sourceWallet, CurrencyWallet _destinationWallet, IDataStore _dataStore, string _url, string _url2)
            : base(_sourceWallet, _destinationWallet, _dataStore, _url)
        {
            url = _url;
            url2 = _url2;
        }
    }
}
