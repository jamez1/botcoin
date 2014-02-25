using System;
using CCXSharp.MtGox.Models;

namespace CCXSharp.Models
{
    [Serializable]
    public class Ticker
    {
        public string High { get; set; }
        public string Low { get; set; }
        public string Average { get; set; }
        public string Volume { get; set; }
        public string Last { get; set; }
        public string Bid { get; set; }
        public string Ask { get; set; }
        public DateTime TimeStamp { get; set; }

        public Ticker()
        {
            
        }

        public Ticker(MtGoxTicker mtgoxTicker)
        {
            High = mtgoxTicker.High.DisplayShort;
            Low = mtgoxTicker.Low.DisplayShort;
            Average = mtgoxTicker.Average.DisplayShort;
            Volume = mtgoxTicker.Volume.DisplayShort;
            Last = mtgoxTicker.Last.DisplayShort;
            Bid = mtgoxTicker.Buy.DisplayShort;
            Ask = mtgoxTicker.Sell.DisplayShort;
            TimeStamp = mtgoxTicker.TimeStamp;
        }
    }
}
