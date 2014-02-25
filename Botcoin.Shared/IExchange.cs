using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;

namespace Botcoin
{
    public interface IExchange
    {
        void UpdateQuotes();
        TickDataModel GetLastQuote();
    }
}
