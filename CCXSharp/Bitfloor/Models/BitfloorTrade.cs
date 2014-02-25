using Newtonsoft.Json;

namespace CCXSharp.Bitfloor.Models
{
    public class BitfloorTrade
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "size")]
        public double Size { get; set; }
        [JsonProperty(PropertyName = "provider_side")]
        public long ProviderSide { get; set; }
        [JsonProperty(PropertyName = "timestamp")]
        public double TimeStamp { get; set; }
        [JsonProperty(PropertyName = "seq")]
        public long Sequence { get; set; }
    }
}
