using Newtonsoft.Json;

namespace CCXSharp.BitInstant.Models
{
    public class Fee
    {
        [JsonProperty(PropertyName = "YouReceive")]
        public double YouReceive { get; set; }

        [JsonProperty(PropertyName = "OurRate")]
        public string OurRate { get; set; }

        [JsonProperty(PropertyName = "LocalCurrency")]
        public string LocalCurrency { get; set; }

        [JsonProperty(PropertyName = "YouPay")]
        public double YouPay { get; set; }
    }
}