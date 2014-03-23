using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Shared.Models
{
    public class Trade
    {
        public readonly string tradeDescription;

        public Trade(string _tradeDescription)
        {
            tradeDescription = _tradeDescription;
            Transactions = new List<Transaction>();
        }

        public decimal Execute(decimal amount)
        {
            decimal currentAmount = amount;

            foreach (Transaction t in Transactions)
            {
                t.setInputAmount(currentAmount);
                currentAmount = t.getOutputAmount();
            }

            return currentAmount;
        }

        public List<Transaction> Transactions { get; set; }
    }
}
