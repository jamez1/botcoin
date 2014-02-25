using System;
using System.Collections.Generic;
using System.Linq;
using CCXSharp.BaseClasses;
using CCXSharp.Bitfloor.Models;
using CCXSharp.Models;
using CCXSharp.MtGox.Models;
using RestSharp;

namespace CCXSharp.Bitfloor
{
    public abstract class BitfloorTradeCommands
    {
        private readonly IBitfloorRestClient restClient = new BitfloorRestClient();

        public Ticker GetTicker(Currency currency)
        {
            Ticker ticker = new Ticker();
            L1BookResponse l1BookResponse = restClient.GetResponse<L1BookResponse>("book/L1/1", Method.GET, null, AccessType.Public);
            if (l1BookResponse == null)
                throw new Exception("Failed to deserialize JSON object of type " + typeof(L1BookResponse));
            Ticks ticks = restClient.GetResponse<Ticks>("ticker/1", Method.GET, null, AccessType.Public);
            if (ticks == null)
                throw new Exception("Failed to deserialize JSON object of type " + typeof(Ticks));
            DayInfo dayInfo = restClient.GetResponse<DayInfo>("day-info/1", Method.GET, null, AccessType.Public);
            if (dayInfo == null)
                throw new Exception("Failed to deserialize JSON object of type " + typeof(DayInfo));
            BitfloorTrade[] trades = restClient.GetResponse<BitfloorTrade[]>("trades/1", Method.GET, null, AccessType.Public);
            if (trades == null)
                throw new Exception("Failed to deserialize JSON object of type " + typeof(BitfloorTrade[]));

            ticker.Bid = l1BookResponse.Bid[0].ToString();
            ticker.Ask = l1BookResponse.Ask[0].ToString();
            ticker.Last = ticks.Price.ToString();
            ticker.Low = dayInfo.Low.ToString();
            ticker.High = dayInfo.High.ToString();
            ticker.Volume = dayInfo.Volume.ToString();
            ticker.TimeStamp = DateTime.Now;
            ticker.Average = (trades.Sum(trade => trade.Price * trade.Size) / dayInfo.Volume).ToString();
            return ticker;
        }

        public Depth GetDepth(Currency currency)
        {
            return new Depth();
        }

        public CurrencyInfo GetCurrencyInfo(Currency currency)
        {
            return new CurrencyInfo();
        }

        public MtGoxAccountInfo GetAccountInfo()
        {
            return new MtGoxAccountInfo();
        }

        public List<Order> GetOrders()
        {
            return new List<Order>();
        }

        public OrderCreateResponse CreateOrder(Currency currency, OrderType type, double amount, double? price)
        {
            return new OrderCreateResponse();
        }

        public OrderCancelResponse CancelOrder(Guid oid)
        {
            return new OrderCancelResponse();
        }

        public bool ValidApiKey
        {
            get { return restClient.ValidApiKey; }
        }

        public string APIKey
        {
            get { return restClient.APIKey; }
            set { restClient.APIKey = value; }
        }

        public string APISecret
        {
            get { return restClient.APISecret; }
            set { restClient.APISecret = value; }
        }

        public string APIPassphrase
        {
            get { return restClient.APIPassphrase; }
            set { restClient.APIPassphrase = value; }
        }
    }
}
