using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Shared.Models
{
    public class FixedFeeCurrencyWallet : CurrencyWallet
    {
        decimal _entryFixedFee, _exitFixedFee;
        readonly IExchange _exchange;

        public FixedFeeCurrencyWallet(decimal entryFixedFee, decimal exitFixedFee, IExchange exchange)
        {
            _entryFixedFee = entryFixedFee;
            _exitFixedFee = exitFixedFee;

            _exchange = exchange;
        }

        public override decimal GetExitFee(decimal amount)
        {
            return Math.Max(amount - _entryFixedFee,0);
        }

        public override decimal GetEntryFee(decimal amount)
        {
            return Math.Max(amount - _exitFixedFee,0);
        }

        public override IExchange GetExchange()
        {
            return _exchange;
        }
    }
}
