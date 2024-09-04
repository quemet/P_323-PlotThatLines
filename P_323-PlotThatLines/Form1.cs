using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot.WinForms;

namespace P_323_PlotThatLines
{
    public partial class Form1 : Form
    {

        readonly FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };
        List<Button> buttons = new List<Button>();
        List<Monnaie> bitcoin = new List<Monnaie>();
        List<Monnaie> ethereum = new List<Monnaie>();
        List<Monnaie> solana = new List<Monnaie>();
        List<float> close_bitcoin = new List<float>();
        List<float> close_ethereum = new List<float>();
        List <float> close_solana = new List<float>();
        List<string> texts = new List<string> { "1W", "1M", "6M", "1Y", "2Y", "MAX" };
        const int ONE_WEEK = 7;
        const int ONE_MONTH = 31;
        const int SIX_MONTH = 183;
        const int ONE_YEAR = 356;
        const int TWO_YEAR = 712;

        public ScottPlot.Plottables.Signal DrawGraph(List<float> close_monnaie, double date, string currency)
        {
            var graph = FormsPlot1.Plot.Add.Signal(close_monnaie);
            graph.Data.XOffset = date;
            graph.Data.Period = 1.0;
            graph.Label = currency;
            return graph;
        }

        public void InitiliazeButton(List<string> texts)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                buttons.Add(CreateButton(75, 25, 80 + i * 110, 400, texts[i]));
            }
        }

        public List<float> InitiliazeCloseListWithTime(List<Monnaie> data, int time)
        {
            List<float> floats = new List<float>();
            for(int i=data.Count - 1; i > data.Count - time; i--)
            {
                floats.Add(data[i]._close);
            }
            return floats;
        }

        public List<float> InitiliazeCloseList(List<Monnaie> data)
        {
            List<float> monnaies = new List<float>();

            for (int i = 0; i < data.Count; i++)
            {
                monnaies.Add(data[i]._close);
            }

            return monnaies;
        }

        public Button CreateButton(int width, int height, int x, int y, string text)
        {
            Button b = new Button { Size = new Size(width, height), Location = new Point(x, y), Text = text, BackColor = Color.LightGray };
            b.Click += B_Click;
            Controls.Add(b);
            return b;
        }

        private void B_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.BackColor = Color.LightBlue;

            FormsPlot1.Plot.Clear();
            switch (b.Text)
            {
                case "1W":
                    List<float> bits_oneweek = InitiliazeCloseListWithTime(bitcoin, ONE_WEEK);
                    List<float> eths_oneweek = InitiliazeCloseListWithTime(ethereum, ONE_WEEK);
                    //List<float> sols_oneweek = InitiliazeCloseListWithTime(solana, ONE_WEEK);

                    DrawGraph(bits_oneweek, bitcoin[bitcoin.Count - ONE_WEEK]._date.ToOADate(), "bitcoin");
                    DrawGraph(eths_oneweek, ethereum[ethereum.Count - ONE_WEEK]._date.ToOADate(), "ethereum");
                    //DrawGraph(sols_oneweek, solana[solana.Count - ONE_WEEK]._date.ToOADate(), "solana");

                    FormsPlot1.Plot.Axes.DateTimeTicksBottom();
                    FormsPlot1.Refresh();
                    break;
                case "1M":
                    List<float> bits_onemonth = InitiliazeCloseListWithTime(bitcoin, ONE_MONTH);
                    List<float> eths_onemonth = InitiliazeCloseListWithTime(ethereum, ONE_MONTH);
                    //List<float> sols_onemonth = InitiliazeCloseListWithTime(solana, ONE_MONTH);

                    DrawGraph(bits_onemonth, bitcoin[bitcoin.Count - ONE_MONTH]._date.ToOADate(), "bitcoin");
                    DrawGraph(eths_onemonth, ethereum[ethereum.Count - ONE_MONTH]._date.ToOADate(), "ethereum");
                    //DrawGraph(sols_onemonth, solana[solana.Count - ONE_MONTH]._date.ToOADate(), "solana");

                    FormsPlot1.Plot.Axes.DateTimeTicksBottom();
                    FormsPlot1.Refresh();
                    break;
                case "6M":
                    List<float> bits_sixmonth = InitiliazeCloseListWithTime(bitcoin, SIX_MONTH);
                    var graph_sixmonth = FormsPlot1.Plot.Add.Signal(bits_sixmonth);
                    graph_sixmonth.Data.XOffset = bitcoin[bitcoin.Count - SIX_MONTH]._date.ToOADate();
                    graph_sixmonth.Data.Period = 1.0;
                    FormsPlot1.Plot.Axes.DateTimeTicksBottom();
                    FormsPlot1.Refresh();
                    break;
                case "1Y":
                    List<float> bits_oneyear = InitiliazeCloseListWithTime(bitcoin, ONE_YEAR);
                    List<float> eths_oneyear = InitiliazeCloseListWithTime(ethereum, ONE_YEAR);

                    DrawGraph(bits_oneyear, bitcoin[bitcoin.Count - ONE_YEAR]._date.ToOADate(), "bitcoin");

                    FormsPlot1.Plot.Axes.DateTimeTicksBottom();
                    FormsPlot1.Refresh();
                    break;
                case "2Y":
                    List<float> bits_twoyear = InitiliazeCloseListWithTime(bitcoin, TWO_YEAR);
                    var graph_twoyear = FormsPlot1.Plot.Add.Signal(bits_twoyear);
                    graph_twoyear.Data.XOffset = bitcoin[bitcoin.Count - TWO_YEAR]._date.ToOADate();
                    graph_twoyear.Data.Period = 1.0;
                    FormsPlot1.Plot.Axes.DateTimeTicksBottom();
                    FormsPlot1.Refresh();
                    break;
                case "MAX":
                    List<float> bits = InitiliazeCloseList(bitcoin);
                    var graph = FormsPlot1.Plot.Add.Signal(bits);
                    graph.Data.XOffset = bitcoin[0]._date.ToOADate();
                    graph.Data.Period = 1.0;
                    FormsPlot1.Plot.Axes.DateTimeTicksBottom();
                    FormsPlot1.Refresh();
                    break;
            }

            foreach(Button button in buttons)
            {
                if(button.Text != b.Text)
                {
                    button.BackColor = Color.LightGray;
                }
            }
        }

        public static List<Monnaie> readPath(string path, string monnaie)
        {
            List<Monnaie> data = new List<Monnaie>();
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');
                    data.Add(new Monnaie(values[0], float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]), float.Parse(values[4]), Convert.ToInt64(values[5]), values[6], monnaie));
                }
            }
            return data;
        }

        public Form1()
        {
            InitializeComponent();

            panel1.Controls.Add(FormsPlot1);

            bitcoin = readPath("../../bitcoin.csv", "bitcoin");
            ethereum = readPath("../../ethereum.csv", "ethereum");
            solana = readPath("../../solana.csv", "solana");

            close_bitcoin = InitiliazeCloseList(bitcoin);
            close_ethereum = InitiliazeCloseList(ethereum);
            close_solana = InitiliazeCloseList(solana);

            InitiliazeButton(texts);

            DrawGraph(close_bitcoin, bitcoin[0]._date.ToOADate(), "bitcoin");
            DrawGraph(close_ethereum, ethereum[0]._date.ToOADate(), "ethereum");
            DrawGraph(close_solana, solana[0]._date.ToOADate(), "solana");

            FormsPlot1.Plot.Axes.DateTimeTicksBottom();

            FormsPlot1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class Monnaie
    {
        public DateTime _date { get; set; }
        public long _dateLong { get; set; }
        public float _open { get; set; }
        public float _high { get; set; }
        public float _low { get; set; }
        public float _close { get; set; }
        public Int64 _volume { get; set; }
        public string _currency { get; set; }
        public string _monnaie { get; set; }

        public Monnaie(string date, float open, float high, float low, float close, Int64 volume, string currency, string monnaie)
        {
            string[] date_split = date.Split('-');
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
}
