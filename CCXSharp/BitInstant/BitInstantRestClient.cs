using CCXSharp.BaseClasses;
using CCXSharp.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace CCXSharp.BitInstant
{
    public interface IBitInstantRestClient : ICCXRestSharp
    {
        IRestResponse CCXRestSharpRequest(string endpoint, Method method, object parameters, AccessType accessType);
    }

    class BitInstantRestClient : CCXRestSharpBase, IBitInstantRestClient
    {
        public BitInstantRestClient() : base(@"https://www.bitinstant.com", @"/api/json/")
        {
        }

        protected override RestRequest CCXRestSharpAuthenticate(RestRequest req, object parameters, AccessType accessType)
        {
            if (parameters != null)
            {
                req.RequestFormat = DataFormat.Json;
                string body = JsonConvert.SerializeObject(parameters);
                req.AddParameter("application/json; charset=utf-8", body, ParameterType.RequestBody);
            }
            return req;
        }
    }
}
