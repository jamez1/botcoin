using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CCXSharp.Interfaces;
using CCXSharp.Models;
using CCXSharp.MtGox.Models;
using Cinch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocketIOClient;
using SocketIOClient.Messages;

namespace CCXSharp.MtGox
{
    public class MtGoxExchange : MtGoxTradeCommands, IMtGoxExchange
    {
        private Client _client;
        private bool ConnectionRunning { get; set; }
        private const string TradesChannel = "dbf1dee9-4f2e-4a08-8cb7-748919a71b21";
        private const string TickerChannel = "d5f06780-30a8-4a48-a2f8-7ed181b4a13f";
        private const string DepthChannel = "24e67e0d-1cad-4cc0-9e7a-f8523ef460fe";
        private string OrderChannel { get; set; }
        private const string LagChannel = "85174711-be64-4de1-b783-0628995d7914";
        private DateTime LastMessage = DateTime.Now;
        public bool SocketOpen
        {
            get { return (DateTime.Now - LastMessage).TotalMilliseconds > 5000; }
        }

        ~MtGoxExchange()
        {
            StopDataPoller();
        }

        public void StartDataPoller()
        {
            ConnectionRunning = true;
            Task.Factory.StartNew(DataPollerLoop).ContinueWith(t =>
            {
                if (t.Exception != null)
                    Mediator.Instance.NotifyColleagues("Exception", t.Exception.InnerException);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public void StopDataPoller()
        {
            ConnectionRunning = false;
            if (_client != null && _client.IsConnected)
                _client.Close();
        }

        private void DataPollerLoop()
        {
            try
            {
                InitializeSocket();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (ConnectionRunning)
                {
                    try
                    {
                        if (!SocketOpen && sw.ElapsedMilliseconds > 30000)
                        {
                            sw.Restart();
                            Mediator.Instance.NotifyColleagues("Orders", GetOrders());
                            Mediator.Instance.NotifyColleagues("AccountInfo", GetAccountInfo());
                            InitializeSocket();
                        }
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        Mediator.Instance.NotifyColleagues("Exception", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Mediator.Instance.NotifyColleagues("Exception", ex);
            }
        }

        public void InitializeSocket()
        {
            if(_client != null)
                _client.Close();
            _client = new Client(@"https://socketio.mtgox.com");
            _client.Message += SocketClientMessage;
            _client.Error += SocketClientError;
            _client.SocketConnectionClosed += SocketClientConnectionClosed;

            if (ValidApiKey)
            {
                SubscribeUserChannel subUser = new SubscribeUserChannel(GetIdKey());
                JSONMessage userMsg = new JSONMessage(subUser, endpoint: "/mtgox") { Json = new JsonEncodedEventMessage("message", subUser) };
                SubscribeLag subLag = new SubscribeLag();
                JSONMessage lagMsg = new JSONMessage(subLag, endpoint: "/mtgox") { Json = new JsonEncodedEventMessage("message", subLag) };
                _client.On("connect", data =>
                {
                    _client.Send(userMsg);
                    _client.Send(lagMsg);
                });
            }

            _client.Connect("/mtgox");
        }

        private void SocketClientError(object sender, ErrorEventArgs e)
        {
            Mediator.Instance.NotifyColleagues("Exception", new Exception("SocketError: " + e.Message));
        }

        private void SocketClientConnectionClosed(object sender, EventArgs e)
        {
            Mediator.Instance.NotifyColleagues("Exception", new Exception("Socket connection closed"));
        }

        private void SocketClientMessage(object sender, MessageEventArgs args)
        {
            if (args.Message.MessageText != null)
            {
                LastMessage = DateTime.Now;
                JContainer o = (JContainer)JsonConvert.DeserializeObject(args.Message.MessageText);
                string op = o["op"].Value<string>();
                if (op == "remark") 
                    return;
                string channel = o["channel"].Value<string>();
                if (op.Equals("private"))
                {
                    switch (channel)
                    {
                        case TickerChannel:
                            Mediator.Instance.NotifyColleagues("Ticker", new Ticker(JsonConvert.DeserializeObject<MtGoxTicker>(o["ticker"].ToString())));
                            break;
                        case TradesChannel:
                            Mediator.Instance.NotifyColleagues("Trade", JsonConvert.DeserializeObject<Trade>(o["trade"].ToString()));
                            break;
                        case DepthChannel:
                            Mediator.Instance.NotifyColleagues("DepthUpdate", JsonConvert.DeserializeObject<DepthUpdate>(o["depth"].ToString()));
                            break;
                        case LagChannel:
                            if ((long)o["lag"]["age"] == 0 || (o["lag"]["qid"] == null && o["lag"]["stamp"] == null))
                                Mediator.Instance.NotifyColleagues("Lag", new LagChannelResponse{age = 0, qid = new Guid(), stamp = DateTime.Now});
                            else
                                Mediator.Instance.NotifyColleagues("Lag", JsonConvert.DeserializeObject<LagChannelResponse>(o["lag"].ToString()));
                            break;
                        default:
                            if (channel == OrderChannel)
                            {
                                if (o["private"].Value<String>().Equals("wallet"))
                                {
                                    Mediator.Instance.NotifyColleagues("Wallet", JsonConvert.DeserializeObject<WalletResponse>(o["wallet"].ToString()));
                                }
                                else if (o["private"].Value<String>().Equals("user_order"))
                                {
                                    Mediator.Instance.NotifyColleagues("Order", JsonConvert.DeserializeObject<Order>(o["user_order"].ToString()));
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (op == "subscribe" && channel != TickerChannel && channel != TradesChannel && channel != DepthChannel && channel != LagChannel)
                        OrderChannel = channel;
                }
            }
        }

        [Serializable]
        public class SubscribeUserChannel
        {
            public string op = "mtgox.subscribe";
            public string key { get; set; }

            public SubscribeUserChannel(string idkey)
            {
                key = idkey;
            }
        }

        [Serializable]
        public class SubscribeLag
        {
            public string op = "mtgox.subscribe";
            public string type = "lag";
        }
    }
}
