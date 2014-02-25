using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using CCXSharp.BaseClasses;
using CCXSharp.Bitfloor.Models;
using Microsoft.Win32;
using RestSharp;
using RestSharp.Contrib;

namespace CCXSharp.Bitfloor
{
    public interface IBitfloorRestClient
    {
        string APIKey { get; set; }
        string APISecret { get; set; }
        string APIPassphrase { get; set; }
        bool ValidApiKey { get; }
        T GetResponse<T>(string endpoint, Method method, object parameters, AccessType accessType);
    }

    public class BitfloorRestClient : CCXRestSharpBase, IBitfloorRestClient
    {
        public BitfloorRestClient() : base(@"https://api.bitfloor.com", "")
        {
        }

        private const string KeyPath = @"Software\CryptoCoinXchange\Bitfloor";
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

        private string apiPassphrase;
        public string APIPassphrase
        {
            get
            {
                if (!string.IsNullOrEmpty(apiSecret)) return apiPassphrase;
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(KeyPath, true);
                apiPassphrase = regKey == null ? "" : regKey.GetValue("ApiPassphrase").ToString();
                return apiPassphrase;
            }
            set
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(KeyPath, true) ?? Registry.CurrentUser.CreateSubKey(KeyPath);
                if (regKey == null) return;
                regKey.SetValue("ApiPassphrase", value);
                apiPassphrase = value;
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
                    GetResponse<BitfloorAccountInfo>("accounts", Method.POST, null, AccessType.Private);
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

            if (TestedApi && !ValidApiKey)
                throw new MissingFieldException("You must configure your API Key");

            NameValueCollection nvc = parameters as NameValueCollection ?? new NameValueCollection();
            nvc.Add("nonce", DateTime.Now.Ticks.ToString());
            string body = ToQueryString(nvc);

            string sign = getHash(Convert.FromBase64String(APISecret), body);
            req.AddHeader("bitfloor-key", APIKey);
            req.AddHeader("bitfloor-sign", sign);
            req.AddHeader("bitfloor-passphrase", APIPassphrase);
            req.AddHeader("bitfloor-version", "1");
            req.AddHeader("Content-Type", @"application/x-www-form-urlencoded");
            req.AddHeader("Content-Length", body.Length.ToString());
            req.AddBody(body);
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
                return string.Join("&", Array.ConvertAll(parameters.AllKeys, key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(parameters[key]))));
            }

            return "";
        }
    }
}
