using ScottPlot.PlotStyles;

namespace ConsoleApp1
{
    public class Monnaie
    {
        public DateTime _date { get; set; }
        public long _dateLong { get; set; }
        public string _open { get; set; }
        public string _high { get; set; }
        public string _low { get; set; }
        public string _close { get; set; }
        public string _volume { get; set; }
        public string _currency { get; set; }
        public string _monnaie { get; set; }

        public Monnaie(string date, string open, string high, string low, string close, string volume, string currency, string monnaie)
        {
            string[] date_split = date.Split("-");
            this._date = new DateTime(Convert.ToInt32(date_split[0]), Convert.ToInt32(date_split[1]), Convert.ToInt32(date_split[2]));
            this._open = open;
            this._high = high;
            this._low = low;
            this._close = close;
            this._volume = volume;
            this._currency = currency;
            this._monnaie = monnaie;
            this._dateLong = long.Parse(this._date.ToString("yyyyMMdd"));
        }
    }
    internal class Program
    {
        public static List<Monnaie> readPath(string path, string monnaie)
        {
            List<Monnaie> data = new List<Monnaie>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');
                    data.Add(new Monnaie(values[0], values[1], values[2], values[3], values[4], values[5], values[6], monnaie));
                }
            }
            return data;
        }

        public static void Main(string[] args)
        {
            List<Monnaie> bitcoin = readPath("../../../bitcoin.csv", "bitcoin");
            List<Monnaie> etherum = readPath("../../../ethereum.csv", "etheurem");
            List<Monnaie> solona = readPath("../../../solona.csv", "solona");
        }
    }
}