using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared;
using Botcoin.Shared.Models;

namespace Botcoin
{
    //Feel like this class is somehow missing a relationship between the trade strategy
    //It could be structured better this could begin to induce coupling between trade strategy and platform 
    //(as the strategy makes notifications but this notification class is implemented per platform, 
    //given that 'Botcoin.Executable.CommandLine' project is platform)

    //Cohesion is low too as this class will take in strategy specific output notifications.
    

    //At the same time, if emails were to be swapped out for say, event logs, or SMS notifications, that is platform dependant
    //and the structure of the content would have to change, so perhaps this is the correct place for the abstraction of interaction with
    //the Botcoin operator
    public interface INotificationEngine
    {
        void TradeSignal(TickDataModel buyQuote, TickDataModel sellQuote);
        void TradeSignal(Trade trade);

    }
}
