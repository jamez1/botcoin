using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CCXSharp.Interfaces;
using Cinch;
using SocketIOClient;

namespace CCXSharp.Bitfloor
{
    public class BitfloorExchange : BitfloorTradeCommands, IBitfloorExchange
    {
        private Client _client;
        private bool ConnectionRunning { get; set; }
        private DateTime LastMessage = DateTime.Now;

        public bool SocketOpen
        {
            get { return (DateTime.Now - LastMessage).TotalMilliseconds > 5000; }
        }

        ~BitfloorExchange()
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
            _client = new Client(@"https://feed.bitfloor.com");
            _client.Message += SocketClientMessage;
            _client.Error += SocketClientError;
            _client.SocketConnectionClosed += SocketClientConnectionClosed;

            _client.On("order_new", (details) => { });
            _client.Connect("/1");
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
               
            }
        }
    }
}
