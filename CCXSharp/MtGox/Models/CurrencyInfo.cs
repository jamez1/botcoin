using System;
using Newtonsoft.Json;

namespace CCXSharp.MtGox.Models
{
    public class CurrencyInfoResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "data")]
        public CurrencyInfo CurrencyInfo { get; set; }
    }

    public class CurrencyInfo
    {
        [JsonProperty(PropertyName = "currency")]
        public Currency Currency { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "decimals")]
        public int Decimals { get; set; }
        [JsonProperty(PropertyName = "display_decimals")]
        public int DisplayDecimals { get; set; }
        [JsonProperty(PropertyName = "symbol_position")]
        public string SymbolPosition { get; set; }
        [JsonProperty(PropertyName = "@virtual")]
        public string Virtual { get; set; }
        [JsonProperty(PropertyName = "ticker_channel")]
        public Guid TickerChannel { get; set; }
        [JsonProperty(PropertyName = "depth_channel")]
        public Guid DepthChannel { get; set; }
    }
}
