using System;
using System.Collections.Generic;
using System.Linq;
using CCXSharp.HelperClasses;
using Newtonsoft.Json;

namespace CCXSharp.MtGox.Models
{
    [Serializable]
    public class DepthUpdate
    {
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "type")]
        public OrderType Type { get; set; }
        [JsonProperty(PropertyName = "type_str")]
        public string TypeString { get; set; }
        [JsonProperty(PropertyName = "volume")]
        public double Volume { get; set; }
        [JsonProperty(PropertyName = "price_int")]
        public long PriceInt { get; set; }
        [JsonProperty(PropertyName = "volume_int")]
        public long VolumeInt { get; set; }
        [JsonProperty(PropertyName = "item")]
        public OrderItem Item { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public Currency Currency { get; set; }
        [JsonProperty(PropertyName = "now")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp { get; set; }
        [JsonProperty(PropertyName = "total_volume_int")]
        public long TotalVolume { get; set; }
    }

    public class DepthResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "data")]
        public Depth Depth { get; set; }
    }

    [Serializable]
    public class DepthItem
    {
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }
        [JsonProperty(PropertyName = "price_int")]
        public long PriceInt { get; set; }
        [JsonProperty(PropertyName = "amount_int")]
        public long AmountInt { get; set; }
        [JsonProperty(PropertyName = "stamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp { get; set; }
        public OrderType Type { get; set; }
    }

    public class Depth
    {
        [JsonProperty(PropertyName = "asks")]
        public List<DepthItem> Asks { get; set; }
        [JsonProperty(PropertyName = "bids")]
        public List<DepthItem> Bids { get; set; }
        [JsonProperty(PropertyName = "filter_min_price")]
        public TickerItem FilterMinPrice { get; set; }
        [JsonProperty(PropertyName = "filter_max_price")]
        public TickerItem FilterMaxPrice { get; set; }

        public double WeightedAverageAskPrice
        {
            get
            {
                double weightedOrderPrice = Asks.Sum(od => od.Amount * od.Price);
                double totalVolume = Asks.Sum(od => od.Amount);
                return weightedOrderPrice / totalVolume;
            }
        }

        public double WeightedAverageBidPrice
        {
            get
            {
                double weightedOrderPrice = Bids.Sum(od => od.Amount * od.Price);
                double totalVolume = Bids.Sum(od => od.Amount);
                return weightedOrderPrice / totalVolume;
            }
        }

        public double LowestAsk
        {
            get
            {
                return Asks.Select(od => od.Price).Min();
            }
        }

        public double HighestBid
        {
            get
            {
                return Bids.Select(od => od.Price).Max();
            }
        }
    } 
}
