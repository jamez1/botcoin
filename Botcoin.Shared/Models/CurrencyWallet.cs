using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared.Enums;

namespace Botcoin.Shared.Models
{
    public abstract class CurrencyWallet
    {
        public abstract IExchange GetExchange();

        public CurrencyType Currency { get; set; }

        public abstract decimal GetExitFee(decimal amount);

        public abstract decimal GetEntryFee(decimal amount);
    }
}
