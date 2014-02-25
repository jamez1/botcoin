using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Adapters.MtGox;
using Botcoin.Shared;
using Botcoin.DataStore;
using Microsoft.Practices.Unity;
using Botcoin.Strategy.Arbitrarge1;
using System.Threading;

namespace Botcoin
{
    class Program
    {
        static Object strategyLock = new Object();
        static UnityContainer _container;
        static bool running = true;

        static void Run()
        {
            var exchanges = _container.ResolveAll<IExchange>().ToArray();
            ITradeStrategy strategy = _container.Resolve<ITradeStrategy>();
            Task[] exchangeUpdateTasks = new Task[exchanges.Count()];


            for (int i =0;i< exchanges.Count();i++)
            {
                var exchange = exchanges[i];

                var execution = Task.Factory.StartNew(() =>
                {
                    while (running)
                    {
                        exchange.UpdateQuotes();

                        lock(strategyLock)
                        {
                            strategy.Execute();
                        }
                    }
                });
                exchangeUpdateTasks[i] = execution;
            }

            //Use array to minimize main thread startup
            Task.WaitAll(exchangeUpdateTasks);

        }

        static void Main(string[] args)
        {
            _container = new UnityContainer();

            _container.RegisterType<IExchange, MtGoxExchange>("MtGoxExchange");
            _container.RegisterType<IDataStore, DBDataStore>();

            _container.RegisterType<ITradeStrategy, Arbitrarge1TradeStrategy>();

            Run();
        }
    }
}
