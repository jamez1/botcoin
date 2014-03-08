using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin
{
    class NullNotificationEngine : INotificationEngine
    {
        public NullNotificationEngine()
        {
        }

        public void TradeSignal(Shared.TickDataModel buyQuote, Shared.TickDataModel sellQuote)
        {
            //DO nothing, used for debugging without spamming the recipients
        }


        public void TradeSignal(Shared.Models.Trade trade)
        {
            //DO nothing, used for debugging without spamming the recipients
        }
    }
}
