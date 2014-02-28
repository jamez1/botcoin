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
using Botcoin.Adapters.Bitstamp;
using Botcoin.Adapters.BTCMarkets;
using Botcoin.Shared.Interfaces;
using Botcoin.Shared.Helpers;
using Botcoin.Adapters.BTCe;
using Botcoin.Adapters.CampBXExchange;

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

        static void Intro()
        {
            Console.WriteLine(
@"
-----WELCOME TO BOTCOIN----
Automated BTC Trading platform
");
        }

        static void Main(string[] args)
        {
            _container = new UnityContainer();

            _container.RegisterType<IExchange, MtGoxExchange>("MtGoxExchange", new ContainerControlledLifetimeManager(), new InjectionMember[]{});
            _container.RegisterType<IExchange, BitStampExchange>("BitStampExchange", new ContainerControlledLifetimeManager(), new InjectionMember[] { });
            _container.RegisterType<IExchange, BTCMarketsExchange>("BTCMarketsExchange", new ContainerControlledLifetimeManager(), new InjectionMember[] { });
            _container.RegisterType<IExchange, BTCeExchange>("BTCeExchange", new ContainerControlledLifetimeManager(), new InjectionMember[] { });
            _container.RegisterType<IExchange, CampBXExchange>("CampBXExchange", new ContainerControlledLifetimeManager(), new InjectionMember[] { });
            _container.RegisterType<IDataStore, DBDataStore>( new ContainerControlledLifetimeManager());

            _container.RegisterType<ITradeStrategy, Arbitrarge1TradeStrategy>(new ContainerControlledLifetimeManager());

            _container.RegisterType<ISMTPProvider, SMTPProvider>(new ContainerControlledLifetimeManager());

            //_container.RegisterType<INotificationEngine, NotificationEngine>(new ContainerControlledLifetimeManager());
            _container.RegisterType<INotificationEngine, NullNotificationEngine>(new ContainerControlledLifetimeManager());

            _container.RegisterType<IConfigurationManager, ConfigurationManager>(new ContainerControlledLifetimeManager());

            Intro();
            Run();
        }
    }
}
