using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Shared.Models
{
    //May not be the best way to implement this, adds real time reporting of transactions
    //at the expense of overhead
    public class Transaction
    {
        readonly CurrencyWalletPair pair;

        public Transaction(CurrencyWalletPair _pair)
        {
            pair = _pair;
        }

        /*
        public string ToString();
        public void Execute();*/

        public void setInputAmount(decimal input)
        {
            sourceAmount = input;
        }

        public decimal getTransactionCost(decimal input)
        {
            return pair.GetTransactionCost(input);
        }

        public decimal getOutputAmount()
        {
            var transactionCosts = getTransactionCost(sourceAmount);
            return sourceAmount - transactionCosts;
        }

        decimal sourceAmount;
        decimal destinationAmount;

    }
}
