using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CCXSharp.Bitfloor.Models
{
    public class L1BookResponse
    {
        [JsonProperty(PropertyName = "bid")]
        public double[] Bid { get; set; }
        [JsonProperty(PropertyName = "ask")]
        public double[] Ask { get; set; }
        [JsonProperty(PropertyName = "seq")]
        public long Sequence { get; set; }
    }
}
