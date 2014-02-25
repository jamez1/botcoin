using System;
using CCXSharp.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace CCXSharp.BaseClasses
{
    public abstract class CCXRestSharpBase : ICCXRestSharp
    {
        protected RestClient CCXRestClient;
        protected static readonly object RestLock = new object();

        protected CCXRestSharpBase(string url, string api_resource)
        {
            CCXRestClient = new RestClient(url + api_resource);
        }

        protected abstract RestRequest CCXRestSharpAuthenticate(RestRequest req, object parameters, AccessType accessType);

        public IRestResponse CCXRestSharpRequest(string endpoint, Method method, object parameters, AccessType accessType)
        {
            lock (RestLock)
            {
                RestRequest req = CCXRestSharpAuthenticate(new RestRequest(endpoint, method), parameters, accessType);
                return CCXRestClient.Execute(req);
            }
        }

        public T GetResponse<T>(string endpoint, Method method, object parameters, AccessType accessType)
        {
            return getObject<T>(CCXRestSharpRequest(endpoint, method, parameters, accessType));
        }

        private T getObject<T>(IRestResponse response)
        {
            if(response.Content.Contains("error"))
                throw new Exception("Response contained error: " + response.Content);

            T jsonObj = JsonConvert.DeserializeObject<T>(response.Content);

            if (jsonObj == null)
                throw new Exception("Failed to deserialize JSON object of type " + typeof(T));
            return jsonObj;
        }
    }

    public enum AccessType
    {
        Private,
        Public
    }
}
