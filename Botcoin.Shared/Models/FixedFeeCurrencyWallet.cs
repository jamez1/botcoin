using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared.Enums;

namespace Botcoin.Shared.Models
{
    public class FixedFeeCurrencyWallet : CurrencyWallet
    {
        decimal _entryFixedFee, _exitFixedFee;
        readonly IExchange _exchange;

        public FixedFeeCurrencyWallet(decimal entryFixedFee, decimal exitFixedFee, CurrencyType currencyType, IExchange exchange)
        {
            _entryFixedFee = entryFixedFee;
            _exitFixedFee = exitFixedFee;
            Currency = currencyType;

            _exchange = exchange;
        }

        public override decimal GetExitFee(decimal amount)
        {
            return _exitFixedFee;
        }

        public override decimal GetEntryFee(decimal amount)
        {
            return _entryFixedFee;
        }

        public override IExchange GetExchange()
        {
            return _exchange;
        }
    }
}
