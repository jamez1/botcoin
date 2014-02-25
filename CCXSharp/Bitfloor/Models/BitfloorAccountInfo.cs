using Newtonsoft.Json;

namespace CCXSharp.Bitfloor.Models
{
    public class BitfloorAccountInfo
    {
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }
        [JsonProperty(PropertyName = "hold")]
        public double Hold { get; set; }
    }
}
