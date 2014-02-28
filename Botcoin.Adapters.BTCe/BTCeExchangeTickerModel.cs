using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Adapters.BTCe
{
    public class BTCeExchangeTickerContainerModel
    {
        public BTCeExchangeTickerModel ticker { get; set; }
    }

    public class BTCeExchangeTickerModel
    {
        public decimal high { get; set; }
        public decimal low         {get;set;}
        public decimal avg         {get;set;}
        public decimal vol         {get;set;}
        public decimal vol_cur     {get;set;}
        public decimal last        {get;set;}
        public decimal buy         {get;set;}
        public decimal sell        {get;set;}
        public int updated     {get;set;}
        public int server_time {get;set;}
    }
}
