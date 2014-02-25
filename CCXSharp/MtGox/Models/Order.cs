using System;
using System.Collections.Generic;
using System.ComponentModel;
using CCXSharp.HelperClasses;
using Newtonsoft.Json;

namespace CCXSharp.MtGox.Models
{
    public class OrderResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<Order> Orders { get; set; }
    }

    [Serializable]
    public class Order : INotifyPropertyChanged
    {
        private Guid oid;
        private Currency currency;
        private OrderItem item;
        private OrderType type;
        private TickerItem amount;
        private TickerItem effectiveamount;
        private TickerItem price;
        private OrderStatus status;
        private DateTime date;

        [JsonProperty(PropertyName = "oid")]
        public Guid OID
        {
            get { return oid; }
            set
            {
                oid = value;
                RaisePropertyChangedEvent("OID");
            }
        }
        [JsonProperty(PropertyName = "currency")]
        public Currency Currency
        {
            get { return currency; }
            set
            {
                currency = value;
                RaisePropertyChangedEvent("Currency");
            }
        }
        [JsonProperty(PropertyName = "item")]
        public OrderItem Item
        {
            get { return item; }
            set
            {
                item = value;
                RaisePropertyChangedEvent("Item");
            }
        }
        [JsonProperty(PropertyName = "type")]
        public OrderType Type
        {
            get { return type; }
            set
            {
                type = value;
                RaisePropertyChangedEvent("Type");
            }
        }
        [JsonProperty(PropertyName = "amount")]
        public TickerItem Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                RaisePropertyChangedEvent("Amount");
            }
        }
        [JsonProperty(PropertyName = "effective_amount")]
        public TickerItem EffectiveAmount
        {
            get { return effectiveamount; }
            set
            {
                effectiveamount = value;
                RaisePropertyChangedEvent("OID");
            }
        }
        [JsonProperty(PropertyName = "price")]
        public TickerItem Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChangedEvent("Price");
            }
        }
        [JsonProperty(PropertyName = "status")]
        public OrderStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChangedEvent("Status");
            }
        }
        [JsonProperty(PropertyName = "date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                RaisePropertyChangedEvent("Date");
            }
        }

        public string Total
        {
            get { return string.Format("${0:0.00}", Price.Value * Amount.Value); }
        }

        public override string ToString()
        {
            return string.Format("OrderID:{0}, Currency:{1}, Item:{2}, Type:{3}, Amount:{4}, EffectiveAmount:{5}, Price:{6}, Status:{7}, Date:{8}",
                OID, Currency, Item, Type, Amount.Display, EffectiveAmount.Display, Price.Display, Status, Date);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            return Equals((Order)obj);
        }

        public bool Equals(Order order)
        {
            return oid == order.oid;
        }

        public override int GetHashCode()
        {
            return oid.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
    }

    public class OrderCreateResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "data")]
        public Guid OID { get; set; }

        public override string ToString()
        {
            return OID.ToString();
        }
    }

    public class OrderCancelResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "data")]
        public OrderIDs IDs { get; set; }
    }

    public class OrderIDs
    {
        [JsonProperty(PropertyName = "oid")]
        public Guid OID { get; set; }
        [JsonProperty(PropertyName = "qid")]
        public Guid QID { get; set; }
    }

    public class WalletResponse 
    {
        [JsonProperty(PropertyName = "op")]
        public OrderOperation Op { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public TickerItem Amount { get; set; }
        [JsonProperty(PropertyName = "info")]
        public string Info { get; set; }
        [JsonProperty(PropertyName = "balance")]
        public TickerItem Balance { get; set; }
    }

    public enum OrderOperation
    {
        In, 
        Out, 
        Spent, 
        Earned, 
        Fee,
        Withdraw,
        Deposit
    }
}
