using Newtonsoft.Json;

namespace CCXSharp.Bitfloor.Models
{
    public class Ticks
    {
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "size")]
        public double Size { get; set; }
        [JsonProperty(PropertyName = "timestamp")]
        public double TimeStamp { get; set; }
        [JsonProperty(PropertyName = "seq")]
        public long Sequence { get; set; }
    }

    public class DayInfo
    {
        [JsonProperty(PropertyName = "low")]
        public double Low { get; set; }
        [JsonProperty(PropertyName = "high")]
        public double High { get; set; }
        [JsonProperty(PropertyName = "volume")]
        public double Volume { get; set; }
    }
}
