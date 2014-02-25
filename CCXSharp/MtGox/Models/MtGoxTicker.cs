using System;
using CCXSharp.HelperClasses;
using Newtonsoft.Json;

namespace CCXSharp.MtGox.Models
{
    public class MtGoxTickerResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "data")]
        public MtGoxTicker Ticker { get; set; }
    }

    [Serializable]
    public class MtGoxTicker
    {
        [JsonProperty(PropertyName = "high")]
        public TickerItem High { get; set; }
        [JsonProperty(PropertyName = "low")]
        public TickerItem Low { get; set; }
        [JsonProperty(PropertyName = "avg")]
        public TickerItem Average { get; set; }
        [JsonProperty(PropertyName = "vwap")]
        public TickerItem VWAP { get; set; }
        [JsonProperty(PropertyName = "vol")]
        public TickerItem Volume { get; set; }
        [JsonProperty(PropertyName = "last_local")]
        public TickerItem LastLocal { get; set; }
        [JsonProperty(PropertyName = "last_orig")]
        public TickerItem LastOrig { get; set; }
        [JsonProperty(PropertyName = "last_all")]
        public TickerItem LastAll { get; set; }
        [JsonProperty(PropertyName = "last")]
        public TickerItem Last { get; set; }
        [JsonProperty(PropertyName = "buy")]
        public TickerItem Buy { get; set; }
        [JsonProperty(PropertyName = "sell")]
        public TickerItem Sell { get; set; }
        [JsonProperty(PropertyName = "item")]
        public OrderItem Item { get; set; }
        [JsonProperty(PropertyName = "now")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp { get; set; }

        public override string ToString()
        {
            return string.Format("Last: {0}, High: {1}, Low: {2}, Volume: {3}, Avg: {4}", Last.DisplayShort, High.DisplayShort, Low.DisplayShort, Volume.DisplayShort, Average.DisplayShort);
        }
    }

    [Serializable]
    public class TickerItem
    {
        [JsonProperty(PropertyName = "value")]
        public double Value { get; set; }
        [JsonProperty(PropertyName = "value_int")]
        public long ValueInt { get; set; }
        [JsonProperty(PropertyName = "display")]
        public string Display { get; set; }
        [JsonProperty(PropertyName = "display_short")]
        public string DisplayShort { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public Currency Currency { get; set; }

        public override string ToString()
        {
            return DisplayShort;
        }
    }
}
