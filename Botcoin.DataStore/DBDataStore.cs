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
            Console.WriteLine("Bid: {1}, Ask: {0}",data.Ask,data.Bid);
        }
    }
}
