using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Botcoin.Adapters.CampBXExchange
{
    //http://campbx.com/api/xticker.php
    public class CampBXTickerModel
    {
        [JsonProperty("Last Trade")]
        public decimal Last_Trade { get; set; }
        [JsonProperty("Best Bid")]
        public decimal Best_Bid { get; set; }
        [JsonProperty("Best Ask")]
        public decimal Best_Ask { get; set; }
    }
}
