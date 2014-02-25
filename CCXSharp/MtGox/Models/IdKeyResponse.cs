using Newtonsoft.Json;

namespace CCXSharp.MtGox.Models
{
    public class IdKeyResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "data")]
        public string IdKey { get; set; }

        public override string ToString()
        {
            return IdKey;
        }
    }
}
