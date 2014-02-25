using System;
using Newtonsoft.Json;

namespace CCXSharp.MtGox.Models
{
    public class LagResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "data")]
        public Lag Lag { get; set; }
    }

    public class Lag
    {
        [JsonProperty(PropertyName = "lag")]
        public long InInt { get; set; }
        [JsonProperty(PropertyName = "lag_secs")]
        public double InSeconds { get; set; }
        [JsonProperty(PropertyName = "lag_text")]
        public string Display { get; set; }
    }

    public class LagChannelResponse
    {
        [JsonProperty(PropertyName = "qid")]
        public Guid qid { get; set; }
        [JsonProperty(PropertyName = "stamp")]
        public object stamp { get; set; }
        [JsonProperty(PropertyName = "age")]
        public long age { get; set; }
    }
}
