using System;
using CCXSharp.HelperClasses;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CCXSharp.MtGox.Models
{
    public class TradeResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<Trade> Trades { get; set; }
    }

    public class Trade
    {
        [JsonProperty(PropertyName = "type")]
        public TradeType Type { get; set; }
        [JsonProperty(PropertyName = "date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "tid")]
        public long TID { get; set; }
        [JsonProperty(PropertyName = "amount_int")]
        public long AmountInt { get; set; }
        [JsonProperty(PropertyName = "price_int")]
        public long PriceInt { get; set; }
        [JsonProperty(PropertyName = "item")]
        public OrderItem Item { get; set; }
        [JsonProperty(PropertyName = "price_currency")]
        public Currency PriceCurrency { get; set; }
        [JsonProperty(PropertyName = "trade_type")]
        public OrderType TradeType { get; set; }
        [JsonProperty(PropertyName = "primary")]
        public string Primary { get; set; }
        [JsonProperty(PropertyName = "properties")]
        public string Properties { get; set; }
    }
}
