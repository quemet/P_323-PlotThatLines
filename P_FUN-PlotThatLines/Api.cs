using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace P_FUN_PlotThatLines
{
    public class Api : IForm
    {
        // the object of the graph
        public readonly FormsPlot FormsPlot1;

        // the object of the form
        public readonly Form form1;

        // list of all the data for the bitcoin
        List<Currency> bitcoin = new List<Currency>();

        // list of all the data for the ethereum
        List<Currency> ethereum = new List<Currency>();

        // list of all the data for solana
        List<Currency> solana = new List<Currency>();

        // list of all the close of bitcoin
        List<float> close_bitcoin = new List<float>();

        // list of all the close of ethereum
        List<float> close_ethereum = new List<float>();

        // list of all the close of solana
        List<float> close_solana = new List<float>();

        // a datetimepicker for the start of the graph
        DateTimePicker startPicker = new DateTimePicker();

        // a datetimepicker for the end of the graph
        DateTimePicker endPicker = new DateTimePicker();

        // a button to submit the new date
        Button b_submit = new Button();

        /// <summary>
        /// Draw a graph into the label in the Forms
        /// </summary>
        /// <param name="close_currency">a list of float of the close of every day</param>
        /// <param name="date">first date of the gaph</param>
        /// <param name="currency">currency of the graph</param>
        /// <returns>return an object signal to draw the graph</returns>
        public ScottPlot.Plottables.Signal DrawGraph(List<float> close_currency, double date, string currency)
        {
            var graph = FormsPlot1.Plot.Add.Signal(close_currency);
            graph.Data.XOffset = date;
            graph.Data.Period = 1.0;
            graph.LegendText = currency;
            return graph;
        }

        /// <summary>
        /// Initiliaze the two DateTime Picker to choose the date
        /// </summary>
        public void InitiliazeDateTimePicker()
        {
            startPicker.Location = new Point(50, 400);
            startPicker.Value = bitcoin.First()._date;
            startPicker.MinDate = bitcoin.First()._date;
            startPicker.MaxDate = bitcoin[bitcoin.Count - 2]._date;
            startPicker.Validated += StartPicker_ValueChanged;

            endPicker.Location = new Point(350, 400);
            endPicker.Value = startPicker.Value.AddDays(1);
            endPicker.MinDate = startPicker.Value.AddDays(1);
            endPicker.MaxDate = bitcoin.Last()._date;
            endPicker.Validated += EndPicker_ValueChanged;

            b_submit.Location = new Point(650, 400);
            b_submit.Text = "Submit";
            b_submit.Click += ChangeDate;

            form1.Controls.Add(startPicker);
            form1.Controls.Add(endPicker);
            form1.Controls.Add(b_submit);
        }

        /// <summary>
        /// Function to change the value of the second datetime
        /// </summary>
        /// <param name="sender">Contains a reference of the control/object</param>
        /// <param name="e">Contains all the event data </param>
        private void EndPicker_ValueChanged(object sender, EventArgs e)
        {
            if (endPicker.Value <= startPicker.Value)
            {
                endPicker.Value = startPicker.Value.AddDays(1);
                endPicker.MinDate = startPicker.Value.AddDays(1);
            }
        }

        /// <summary>
        /// Function to change the value of the first datetime
        /// </summary>
        /// <param name="sender">Contains a reference of the control/object</param>
        /// <param name="e">Contains all the event data </param>
        private void StartPicker_ValueChanged(object sender, EventArgs e)
        {
            endPicker.Value = startPicker.Value.AddDays(1);
        }

        /// <summary>
        /// Function to refresh the list currency with startDate and endDate
        /// </summary>
        /// <param name="currency">list of all the currency</param>
        /// <param name="startDate">the date of the start of the graph</param>
        /// <param name="endDate">the date of the end of the graph</param>
        /// <returns>return a list of currency within the two date</returns>
        public List<Currency> RefreshDate(List<Currency> currency, DateTime startDate, DateTime endDate)
        {
            return currency.Where(b => b._date >= startDate && b._date <= endDate).ToList();
        }

        /// <summary>
        /// Redraw the new graph with the new date
        /// </summary>
        /// <param name="sender">Contains a reference of the control/object</param>
        /// <param name="e">Contains all the event data </param>
        public void ChangeDate(object sender, EventArgs e)
        {
            DateTime start_date = startPicker.Value;
            DateTime end_date = endPicker.Value;

            List<Currency> bit = RefreshDate(bitcoin, start_date, end_date);
            List<Currency> eth = RefreshDate(ethereum, start_date, end_date);
            List<Currency> sln = RefreshDate(solana, start_date, end_date);

            FormsPlot1.Plot.Clear();

            DrawGraph(bit.Select(b => b._close).ToList(), ReturnCorrectFormatDate(bit, 0), "bitcoin");

            // Do not plot empty eth data
            if (eth.Count > 0)
            {
                DrawGraph(eth.Select(eths => eths._close).ToList(), ReturnCorrectFormatDate(eth, 0), "ethereum");
            }

            // Do not plot empty sln data
            if (sln.Count > 0)
            {
                DrawGraph(sln.Select(s => s._close).ToList(), ReturnCorrectFormatDate(sln, 0), "solana");
            }

            FormsPlot1.Plot.Axes.DateTimeTicksBottom();

            FormsPlot1.Plot.XLabel("Date");
            FormsPlot1.Plot.YLabel("Prix en dollars américains");

            FormsPlot1.Refresh();
        }

        /// <summary>
        /// Read a file csv and return a list of currency
        /// </summary>
        /// <param name="path">Path to the file csv</param>
        /// <param name="currency">Currency in the file csv</param>
        /// <returns>return a list of Currency from the file</returns>
        public List<Currency> ReadPath(string path, string currency)
        {
            List<Currency> data = new List<Currency>();
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();

                // Loop on the file to retrieve the information
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    List<string> values = line.Split(',').ToList();
                    data.Add(new Currency(values[0], float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]), float.Parse(values[4]), Convert.ToInt64(values[5]), values[6], currency));
                }
            }
            return data;
        }

        public double ReturnCorrectFormatDate(List<Currency> c, int index)
        {
            if(index == 0) return c.First()._date.ToOADate();
            else if(index == c.Count - 1) return c.Last()._date.ToOADate();
            else return c.ElementAt(index)._date.ToOADate();
        }

        /// <summary>
        /// Constructor for the test
        /// </summary>
        public Api()
        {

        }

        /// <summary>
        /// Constructor for the application
        /// </summary>
        /// <param name="Form1">an object of the class Form</param>
        /// <param name="FormsPlot1">an object of the class FormsPlot</param>
        public Api(Form Form1, FormsPlot FormsPlot1 = null)
        {
            form1 = Form1;

            this.FormsPlot1 = FormsPlot1;

            string type = "";

            using(StreamReader sr = new StreamReader(".env"))
            {
                string line = sr.ReadLine();
                type = line.Split('=')[1];
            }

            string path = type == "Developement" ? "../../../" : "./";

            bitcoin = ReadPath(path + "bitcoin.csv", "bitcoin");
            ethereum = ReadPath(path + "ethereum.csv", "ethereum");
            solana = ReadPath(path + "solana.csv", "solana");

            close_bitcoin = bitcoin.Select(b => b._close).ToList();
            close_ethereum = ethereum.Select(e => e._close).ToList();
            close_solana = solana.Select(s => s._close).ToList();

            InitiliazeDateTimePicker();

            DrawGraph(close_bitcoin, ReturnCorrectFormatDate(bitcoin, 0), "bitcoin");
            DrawGraph(close_ethereum, ReturnCorrectFormatDate(ethereum, 0), "ethereum");
            DrawGraph(close_solana, ReturnCorrectFormatDate(solana, 0), "solana");

            FormsPlot1.Plot.Axes.DateTimeTicksBottom();

            FormsPlot1.Plot.XLabel("Date");
            FormsPlot1.Plot.YLabel("Prix en dollars américains");

            FormsPlot1.Refresh();
        }
    }
}
