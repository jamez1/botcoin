using System;
using System.Collections.Specialized;
using System.Text;
using CCXSharp.BaseClasses;
using CCXSharp.Interfaces;
using CCXSharp.MtGox.Models;
using RestSharp;
using System.Security.Cryptography;
using Microsoft.Win32;
using RestSharp.Contrib;

namespace CCXSharp.MtGox
{
    public interface IMtGoxRestClient : ICCXRestSharp
    {
        bool ValidApiKey { get; }
        string APIKey { get; set;  }
        string APISecret { get; set;  }
        IRestResponse CCXRestSharpRequest(string endpoint, Method method, object parameters, AccessType accessType);
        string ToQueryString(NameValueCollection nvc);
    }

    public class MtGoxRestClient : CCXRestSharpBase, IMtGoxRestClient
    {
        public static string lastResponse;

        public MtGoxRestClient() : base(@"https://data.mtgox.com", @"/api/2/")
        {
        }

        private const string KeyPath = @"Software\CryptoCoinXchange\MtGox";
        private bool TestedApi;
        private bool ApiWorks;
        private string apiKey;
        public string APIKey
        {
            get
            {
                if (!string.IsNullOrEmpty(apiKey)) return apiKey;
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(KeyPath, true);
                apiKey = regKey == null ? "" : regKey.GetValue("ApiKey").ToString();
                return apiKey;
            }
            set
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(KeyPath, true) ?? Registry.CurrentUser.CreateSubKey(KeyPath);
                if (regKey == null) return;
                regKey.SetValue("ApiKey", value);
                apiKey = value;
            }
        }

        private string apiSecret;
        public string APISecret
        {
            get
            {
                if (!string.IsNullOrEmpty(apiSecret)) return apiSecret;
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(KeyPath, true);
                apiSecret = regKey == null ? "" : regKey.GetValue("ApiSecret").ToString();
                return apiSecret;
            }
            set
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(KeyPath, true) ?? Registry.CurrentUser.CreateSubKey(KeyPath);
                if (regKey == null) return;
                regKey.SetValue("ApiSecret", value);
                apiSecret = value;
            }
        }

        public bool ValidApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(APIKey) || string.IsNullOrEmpty(APISecret))
                    return false;

                if (TestedApi) return ApiWorks;

                try
                {
                    GetResponse<MtGoxAccountInfoResponse>("money/info", Method.POST, null, AccessType.Private);
                    ApiWorks = true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    TestedApi = true;
                }

                return true;
            }
        }

        protected override RestRequest CCXRestSharpAuthenticate(RestRequest req, object parameters, AccessType accessType)
        {
            if (accessType == AccessType.Public) return req;

            if(TestedApi && !ValidApiKey)
                throw new MissingFieldException("You must configure your API Key");

            Int64 nonce = DateTime.Now.Ticks;

            string endpoint = req.Resource;
            string post = "nonce=" + nonce;
            req.AddParameter("nonce", nonce);
            NameValueCollection nvc = parameters as NameValueCollection;
            if (nvc != null)
            {
                post += ToQueryString(nvc);
                foreach (string s in nvc.Keys)
                {
                    req.AddParameter(s, nvc[s]);
                }
            }
            string prefix = endpoint;

            string sign = getHash(Convert.FromBase64String(apiSecret), prefix + Convert.ToChar(0) + post);
     
            req.AddHeader("Rest-Key", apiKey);
            req.AddHeader("Rest-Sign", sign);
            return req;
        }

        private string getHash(byte[] keyByte, String message)
        {
            var hmacsha512 = new HMACSHA512(keyByte);
            var messageBytes = Encoding.UTF8.GetBytes(message);
            return Convert.ToBase64String(hmacsha512.ComputeHash(messageBytes));
        }

        public string ToQueryString(NameValueCollection parameters)
        {
            if (parameters != null)
            {
                return "&" + string.Join("&", Array.ConvertAll(parameters.AllKeys, key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(parameters[key]))));
            }

            return "";
        }
    }
}
