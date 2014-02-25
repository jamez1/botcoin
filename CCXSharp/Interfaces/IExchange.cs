using System;
using System.Collections.Generic;
using CCXSharp.Models;
using CCXSharp.MtGox.Models;

namespace CCXSharp.Interfaces
{
    public interface IExchange
    {
        Ticker GetTicker(Currency currency);
        Depth GetDepth(Currency currency);
        CurrencyInfo GetCurrencyInfo(Currency currency);
        MtGoxAccountInfo GetAccountInfo();
        List<Order> GetOrders();
        OrderCreateResponse CreateOrder(Currency currency, OrderType type, double amount, double? price);
        OrderCancelResponse CancelOrder(Guid oid);
        void StartDataPoller();
        void StopDataPoller();
        bool ValidApiKey { get; }
    }

    public interface IMtGoxExchange : IExchange
    {
        Lag GetLag();
        TradeResponse GetTrades(Currency currency);
        TradeResponse GetTrades(Currency currency, DateTime? fromDateTime);
        string APIKey { get; set; }
        string APISecret { get; set; }
        string GetIdKey();
        bool SocketOpen { get; }
    }

    public interface IBitfloorExchange : IExchange
    {
        string APIKey { get; set; }
        string APISecret { get; set; }
        string APIPassphrase { get; set; }
    }
}
