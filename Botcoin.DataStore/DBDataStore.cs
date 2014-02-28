using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;

namespace Botcoin.DataStore
{
    public class DBDataStore : IDataStore
    {
        public DBDataStore()
        {
        }


        public void Save(TickDataModel data)
        {
            Console.WriteLine("Bid: {1}\t Ask: {0}\t Exchange: {2}",data.Ask,data.Bid, data.SourceExchange);
        }
    }
}
