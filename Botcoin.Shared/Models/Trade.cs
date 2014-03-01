using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Shared.Models
{
    public class Trade
    {
        readonly string tradeDescription

        public Trade(string _tradeDescription)
        {
            tradeDescription = _tradeDescription;
            Transactions = new List<Transaction>();
        }

        public List<Transaction> Transactions { get; set; }
    }
}
