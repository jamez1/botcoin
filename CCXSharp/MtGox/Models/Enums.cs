using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CCXSharp.MtGox.Models
{
    public enum Currency
    {
        BTC,
        USD,
        AUD,
        CAD,
        CHF,
        CNY,
        DKK,
        EUR,
        GBP,
        HKD,
        JPY,
        NZD,
        PLN,
        RUB,
        SEK,
        SGD,
        THB,
        NOK,
        None
    }

    public enum ResponseResult
    {
        Success,
        Error
    }

    public enum Right
    {
        None,
        Get_Info,
        Trade,
        Deposit,
        Withdraw,
        Merchant
    }

    public enum OrderType
    {
        Ask,
        Bid
    }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum OrderStatus
    {
        Pending,
        Executing,
        [EnumMember(Value = "post-pending")]
        PostPending,
        Open,
        Stop,
        Invalid
    }

    public enum OrderItem
    {
        BTC,
        None
    }

    public enum TradeType
    {
        Trade
    }
}
