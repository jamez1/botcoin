using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Shared.Models
{
    public class PercentageFeeCurrencyWallet : CurrencyWallet
    {
        decimal _entryPercentFee, _exitPercentFee;
        IExchange _exchange;

        public PercentageFeeCurrencyWallet(decimal entryPercentFee, decimal exitPercentFee, IExchange exchange)
        {
            _exchange = exchange;
            _entryPercentFee = entryPercentFee;
            _exitPercentFee = exitPercentFee;
        }

        public override decimal GetExitFee(decimal amount)
        {
            return _exitPercentFee * amount;
        }

        public override decimal GetEntryFee(decimal amount)
        {
            return _entryPercentFee * amount;
        }

        public override IExchange GetExchange()
        {
            return _exchange;
        }
    }
}
