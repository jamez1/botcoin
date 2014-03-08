using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared.Models;

namespace Botcoin.Shared.Interfaces
{
    public interface ICurrencyPairRepository
    {
        void Store(CurrencyWalletPair pair);

        IEnumerable<CurrencyWalletPair> GetAll();
    }
}
