using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared.Interfaces;
using Botcoin.Shared.Models;

namespace Botcoin.Shared
{
    public class CurrencyPairRepository: ICurrencyPairRepository
    {
        List<CurrencyWalletPair> _pairs;

        public CurrencyPairRepository()
        {
            _pairs = new List<CurrencyWalletPair>();
        }

        public void Store(CurrencyWalletPair pair)
        {
            _pairs.Add(pair);
        }

        public IEnumerable<CurrencyWalletPair> GetAll()
        {
            return _pairs;
        }
    }
}
