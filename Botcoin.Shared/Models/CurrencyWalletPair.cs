using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Shared.Models
{
    public class CurrencyWalletPair
    {
        public readonly CurrencyWallet sourceWallet;
        public readonly CurrencyWallet destinationWallet;
        protected readonly IDataStore dataStore;

        protected TickDataModel lastTick=null;

        public CurrencyWalletPair(CurrencyWallet _sourceWallet, CurrencyWallet _destinationWallet, IDataStore _dataStore)
        {
            sourceWallet = _sourceWallet;
            destinationWallet = _destinationWallet;
            dataStore = _dataStore;
        }

        public decimal GetTransactionCost(decimal amount, bool fromSource=true)
        {
            decimal exit, entry;

            if (fromSource)
            {
                exit = sourceWallet.GetExitFee(amount);
                entry = destinationWallet.GetEntryFee(amount);
            }
            {
                exit = destinationWallet.GetExitFee(amount);
                entry = sourceWallet.GetEntryFee(amount);
            }

            return exit + entry;
        }

        public TickDataModel LastTick()
        {
            return lastTick;
        }

        public virtual void Tick(TickDataModel tick)
        {
            lastTick = tick;
            dataStore.Save(tick);//TODO: categorize the data by currencywalletpair
        }

    }
}
