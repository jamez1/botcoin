using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CCXSharp.BitInstant.Models
{
    public class PairsResponse
    {
        [JsonProperty(PropertyName = "pairs")]
        public List<String> Pairs { get; set; }
    }

    public class Pair
    {
        [JsonProperty(Order = 0)]
        public CouponType Coupon { get; set; }
        [JsonProperty(Order = 1)]
        public ExchangeName Exchange { get; set; }
        [JsonProperty(Order = 2)]
        public String Fees { get; set; }

    }
}
