using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;
using Botcoin.Shared.Models;

namespace Botcoin.Adapters.BTCe
{
    public class BTCeCurrencyWalletPair : CurrencyWalletPair
    {
        public string url;

        public BTCeCurrencyWalletPair(CurrencyWallet _sourceWallet, CurrencyWallet _destinationWallet, IDataStore _dataStore, string _url)
            : base(_sourceWallet, _destinationWallet, _dataStore)
        {
            url = _url;
        }
    }
}
