using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ScottPlot.WinForms;

namespace P_323_PlotThatLines
{
    interface IForm
    {
        ScottPlot.Plottables.Signal DrawGraph(List<float> close_monnaie, double date, string currency);
    }

    public partial class Form1 : Form
    {
        readonly FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

        static List<Monnaie> bitcoin = readPath("../../bitcoin.csv", "bitcoin");
        static List<Monnaie> ethereum = readPath("../../ethereum.csv", "ethereum");
        static List<Monnaie> solana = readPath("../../solana.csv", "solana");

        List<float> close_bitcoin = bitcoin.Select(b => b._close).ToList();
        List<float> close_ethereum = ethereum.Select(e => e._close).ToList();
        List <float> close_solana = solana.Select(s => s._close).ToList();

        DateTimePicker startPicker = new DateTimePicker();
        DateTimePicker endPicker = new DateTimePicker();

        Button b_submit = new Button();

        public ScottPlot.Plottables.Signal DrawGraph(List<float> close_monnaie, double date, string currency)
        {
            var graph = FormsPlot1.Plot.Add.Signal(close_monnaie);
            graph.Data.XOffset = date;
            graph.Data.Period = 1.0;
            graph.LegendText = currency;
            return graph;
        }

        public void InitiliazeDateTimePicker()
        {
            startPicker.Location = new Point(50, 400);
            startPicker.Value = bitcoin[0]._date;
            startPicker.MinDate = bitcoin[0]._date;
            startPicker.MaxDate = bitcoin[bitcoin.Count - 2]._date;
            startPicker.ValueChanged += StartPicker_ValueChanged;

            endPicker.Location = new Point(350, 400);
            endPicker.Value = startPicker.Value.AddDays(1);
            endPicker.MinDate = startPicker.Value.AddDays(1);
            endPicker.MaxDate = bitcoin[bitcoin.Count - 1]._date;
            endPicker.ValueChanged += EndPicker_ValueChanged;

            b_submit.Location = new Point(650, 400);
            b_submit.Text = "Submit";
            b_submit.Click += ChangeDate;

            this.Controls.Add(startPicker);
            this.Controls.Add(endPicker);
            this.Controls.Add(b_submit);
        }

        private void EndPicker_ValueChanged(object sender, EventArgs e)
        {
            if(endPicker.Value <= startPicker.Value)
            {
                endPicker.Value = startPicker.Value.AddDays(1);
                endPicker.MinDate = startPicker.Value.AddDays(1);
            }
        }

        private void StartPicker_ValueChanged(object sender, EventArgs e)
        {
            endPicker.Value = startPicker.Value.AddDays(1);
        }

        private void ChangeDate(object sender, EventArgs e)
        {
            DateTime start_date = startPicker.Value;
            DateTime end_date = endPicker.Value;

            List<Monnaie> bit_opens = bitcoin.Where(b => b._date >= start_date && b._date < end_date).ToList();
            List<Monnaie> eth_opens = ethereum.Where(eth => eth._date >= start_date && eth._date < end_date).ToList();
            List<Monnaie> sln_opens = solana.Where(s => s._date >= start_date && s._date < end_date).ToList();

            FormsPlot1.Plot.Clear();

            DrawGraph(bit_opens.Select(b => b._close).ToList(), bit_opens[0]._date.ToOADate(), "bitcoin");
            if(eth_opens.Count > 0)
            {
                DrawGraph(eth_opens.Select(eth => eth._close).ToList(), eth_opens[0]._date.ToOADate(), "ethereum");
            }

            if(sln_opens.Count > 0)
            {
                DrawGraph(sln_opens.Select(s => s._close).ToList(), sln_opens[0]._date.ToOADate(), "solana");
            }

            FormsPlot1.Plot.Axes.DateTimeTicksBottom();

            FormsPlot1.Plot.XLabel("Date");
            FormsPlot1.Plot.YLabel("Prix en dollars américains");

            FormsPlot1.Refresh();
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

            InitiliazeDateTimePicker();

            DrawGraph(close_bitcoin, bitcoin[0]._date.ToOADate(), "bitcoin");
            DrawGraph(close_ethereum, ethereum[0]._date.ToOADate(), "ethereum");
            DrawGraph(close_solana, solana[0]._date.ToOADate(), "solana");

            FormsPlot1.Plot.Axes.DateTimeTicksBottom();

            FormsPlot1.Plot.XLabel("Date");
            FormsPlot1.Plot.YLabel("Prix en dollars américains");

            FormsPlot1.Refresh();
        }
    }

    public class Monnaie
    {
        public DateTime _date { get; set; }
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
        }
    }
}
