using System;

namespace P_323_PlotThatLines
{
    public class Currency
    {
        // the date of the day
        public DateTime _date { get; set; }

        // the first price of the day
        public float _open { get; set; }

        // the highest price of the day
        public float _high { get; set; }

        // the lowest price of the day
        public float _low { get; set; }

        // the last price of the day
        public float _close { get; set; }

        // number of currency sell
        public Int64 _volume { get; set; }

        // name of the currency like euro or chf
        public string _currency { get; set; }

        // name of the cryptocurrency
        public string _cur { get; set; }

        /// <summary>
        /// Only constructor of the class
        /// </summary>
        /// <param name="date">date of the day</param>
        /// <param name="open">first price of the day</param>
        /// <param name="high">highest price of the day</param>
        /// <param name="low">lowest price of the day</param>
        /// <param name="close">last price of the day</param>
        /// <param name="volume">number of currency sell</param>
        /// <param name="currency">name of the currency ex(Dollars, Euro)</param>
        /// <param name="cur">name of the cryptocurrency</param>
        public Currency(string date, float open, float high, float low, float close, Int64 volume, string currency, string cur)
        {
            string[] date_split = date.Split('-');
            this._date = new DateTime(Convert.ToInt32(date_split[0]), Convert.ToInt32(date_split[1]), Convert.ToInt32(date_split[2]));
            this._open = open;
            this._high = high;
            this._low = low;
            this._close = close;
            this._volume = volume;
            this._currency = currency;
            this._cur = cur;
        }
    }
}
