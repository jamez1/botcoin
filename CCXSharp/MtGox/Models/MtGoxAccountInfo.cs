using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCXSharp.MtGox.Models
{
    public class MtGoxAccountInfoResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "data")]
        public MtGoxAccountInfo AccountInfo { get; set; }
    }

    public class MtGoxAccountInfo
    {
        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; }
        [JsonProperty(PropertyName = "Index")]
        public long Index { get; set; }
        [JsonProperty(PropertyName = "Id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "Rights")]
        public List<Right> Rights { get; set; }
        [JsonProperty(PropertyName = "Language")]
        public string Language { get; set; }
        [JsonProperty(PropertyName = "Created")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Created { get; set; }
        [JsonProperty(PropertyName = "Last_Login")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime LastLogin { get; set; }
        [JsonProperty(PropertyName = "Wallets")]
        public Wallets Wallets { get; set; }
        [JsonProperty(PropertyName = "Monthly_Volume")]
        public InfoItem MonthlyVolume { get; set; }
        [JsonProperty(PropertyName = "Trade_Fee")]
        public double TradeFee { get; set; }
    }

    public class Wallets
    {
        [JsonProperty(PropertyName = "BTC")]
        public WalletInfo BTC { get; set; }
        [JsonProperty(PropertyName = "USD")]
        public WalletInfo USD { get; set; }
    }

    public class WalletInfo
    {
        [JsonProperty(PropertyName = "Balance")]
        public InfoItem Balance { get; set; }
        [JsonProperty(PropertyName = "Operations")]
        public int Operations { get; set; }
        [JsonProperty(PropertyName = "Daily_Withdraw_Limit")]
        public InfoItem DailyWithdrawLimit { get; set; }
        [JsonProperty(PropertyName = "Monthly_Withdraw_Limit")]
        public InfoItem MonthlyWithdrawLimit { get; set; }
        [JsonProperty(PropertyName = "Max_Withdraw")]
        public InfoItem MaxWithdraw { get; set; }
        [JsonProperty(PropertyName = "Open_Orders")]
        public InfoItem OpenOrders { get; set; }
    }

    public class InfoItem
    {
        [JsonProperty(PropertyName = "value")]
        public double Value { get; set; }
        [JsonProperty(PropertyName = "value_int")]
        public long ValueInt { get; set; }
        [JsonProperty(PropertyName = "display")]
        public string Display { get; set; }
        [JsonProperty(PropertyName = "display_short")]
        public string DisplayShort { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public Currency Currency { get; set; }

        public override string ToString()
        {
            return DisplayShort;
        }
    }
}
