using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCXSharp.BitInstant.Models
{
    public class QuoteRequest
    {
        [JsonProperty(PropertyName = "dest_account")]
        public string DestAccount { get; set; }

        [JsonProperty(PropertyName = "notify_email")]
        public string NotifyEmail { get; set; }

        [JsonProperty(PropertyName = "dob")]
        public string DoB { get; set; }

        [JsonProperty(PropertyName = "dest_exchange")]
        public string DestExchange { get; set; }

        [JsonProperty(PropertyName = "lName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "fName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "pay_method")]
        public string PayMethod { get; set; }
    }

    public class Quote
    {
        [JsonProperty(PropertyName = "EventID")]
        public Guid EventID { get; set; }

        [JsonProperty(PropertyName = "DestAccount")]
        public string DestAccount { get; set; }

        [JsonProperty(PropertyName = "OurRate")]
        public string OurRate { get; set; }

        [JsonProperty(PropertyName = "AmountCreditedToClientAccountUSD")]
        public double AmountCreditedToClientAccountUSD { get; set; }

        [JsonProperty(PropertyName = "DestExchange")]
        public string DestExchange { get; set; }

        [JsonProperty(PropertyName = "PayMethod")]
        public string PayMethod { get; set; }

        [JsonProperty(PropertyName = "AmountPaidByClientUSD")]
        public double AmountPaidByClientUSD { get; set; }

        [JsonProperty(PropertyName = "EventSentAt")]
        public string EventSentAt { get; set; }

        [JsonProperty(PropertyName = "expires")]
        public string Expires { get; set; }

        [JsonProperty(PropertyName = "NotifyEmail")]
        public string NotifyEmail { get; set; }

        [JsonProperty(PropertyName = "ExchangeName")]
        public string ExchangeName { get; set; }

        [JsonProperty(PropertyName = "eventtype")]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "ExpectedMaxDeliveryTime")]
        public double ExpectedMaxDeliveryTime { get; set; }

        [JsonProperty(PropertyName = "GuaranteedDeliveryTime")]
        public double GuaranteedDeliveryTime { get; set; }
    }
}
