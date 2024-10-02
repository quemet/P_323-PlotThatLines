using ScottPlot.WinForms;

namespace P_FUN_PlotThatLines
{
    public class FormPlotter
    {
        // the object of the graph
        public readonly FormsPlot FormsPlot1;

        // the object of the form
        public readonly Form form1;

        internal static DefaultDataHandler DataHandler = new();

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
            DataHandler.startPicker.Location = new Point(50, 400);
            DataHandler.startPicker.Value = DataHandler.bitcoin.First()._date;
            DataHandler.startPicker.MinDate = DataHandler.bitcoin.First()._date;
            DataHandler.startPicker.MaxDate = DataHandler.bitcoin[DataHandler.bitcoin.Count - 2]._date;
            DataHandler.startPicker.Validated += StartPicker_ValueChanged;

            DataHandler.endPicker.Location = new Point(350, 400);
            DataHandler.endPicker.Value = DataHandler.startPicker.Value.AddDays(1);
            DataHandler.endPicker.MinDate = DataHandler.startPicker.Value.AddDays(1);
            DataHandler.endPicker.MaxDate = DataHandler.bitcoin.Last()._date;
            DataHandler.endPicker.Validated += EndPicker_ValueChanged;

            b_submit.Location = new Point(650, 400);
            b_submit.Text = "Submit";
            b_submit.Click += ChangeDate;

            form1.Controls.Add(DataHandler.startPicker);
            form1.Controls.Add(DataHandler.endPicker);
            form1.Controls.Add(b_submit);
        }

        /// <summary>
        /// Function to change the value of the second datetime
        /// </summary>
        /// <param name="sender">Contains a reference of the control/object</param>
        /// <param name="e">Contains all the event data </param>
        private void EndPicker_ValueChanged(object sender, EventArgs e)
        {
            if (DataHandler.endPicker.Value <= DataHandler.startPicker.Value)
            {
                DataHandler.endPicker.Value = DataHandler.startPicker.Value.AddDays(1);
                DataHandler.endPicker.MinDate = DataHandler.startPicker.Value.AddDays(1);
            }
        }

        /// <summary>
        /// Function to change the value of the first datetime
        /// </summary>
        /// <param name="sender">Contains a reference of the control/object</param>
        /// <param name="e">Contains all the event data </param>
        private void StartPicker_ValueChanged(object sender, EventArgs e)
        {
            DataHandler.endPicker.Value = DataHandler.startPicker.Value.AddDays(1);
        }

        /// <summary>
        /// Redraw the new graph with the new date
        /// </summary>
        /// <param name="sender">Contains a reference of the control/object</param>
        /// <param name="e">Contains all the event data </param>
        public void ChangeDate(object sender, EventArgs e)
        {
            List<List<Currency>> c = DataHandler.ChangeDate(sender, e);

            List<Currency> bit = c[0];
            List<Currency> eth = c[1];
            List<Currency> sln = c[2];

            FormsPlot1.Plot.Clear();

            DrawGraph(bit.Select(b => b._close).ToList(), DataHandler.ReturnCorrectFormatDate(bit, 0), "bitcoin");

            // Do not plot empty eth data
            if (eth.Count > 0)
            {
                DrawGraph(eth.Select(eths => eths._close).ToList(), DataHandler.ReturnCorrectFormatDate(eth, 0), "ethereum");
            }

            // Do not plot empty sln data
            if (sln.Count > 0)
            {
                DrawGraph(sln.Select(s => s._close).ToList(), DataHandler.ReturnCorrectFormatDate(sln, 0), "solana");
            }

            FormsPlot1.Plot.Axes.DateTimeTicksBottom();

            FormsPlot1.Plot.XLabel("Date");
            FormsPlot1.Plot.YLabel("Prix en dollars américains");

            FormsPlot1.Refresh();
        }

        /// <summary>
        /// Constructor for the test
        /// </summary>
        public FormPlotter()
        {

        }

        /// <summary>
        /// Constructor for the application
        /// </summary>
        /// <param name="Form1">an object of the class Form</param>
        /// <param name="FormsPlot1">an object of the class FormsPlot</param>
        public FormPlotter(Form Form1, FormsPlot FormsPlot1 = null)
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

            DataHandler.bitcoin = DataHandler.ReadPath(path + "bitcoin.csv", "bitcoin");
            DataHandler.ethereum = DataHandler.ReadPath(path + "ethereum.csv", "ethereum");
            DataHandler.solana = DataHandler.ReadPath(path + "solana.csv", "solana");

            DataHandler.close_bitcoin = DataHandler.bitcoin.Select(b => b._close).ToList();
            DataHandler.close_ethereum = DataHandler.ethereum.Select(e => e._close).ToList();
            DataHandler.close_solana = DataHandler.solana.Select(s => s._close).ToList();

            InitiliazeDateTimePicker();

            DrawGraph(DataHandler.close_bitcoin, DataHandler.ReturnCorrectFormatDate(DataHandler.bitcoin, 0), "bitcoin");
            DrawGraph(DataHandler.close_ethereum, DataHandler.ReturnCorrectFormatDate(DataHandler.ethereum, 0), "ethereum");
            DrawGraph(DataHandler.close_solana, DataHandler.ReturnCorrectFormatDate(DataHandler.solana, 0), "solana");

            FormsPlot1.Plot.Axes.DateTimeTicksBottom();

            FormsPlot1.Plot.XLabel("Date");
            FormsPlot1.Plot.YLabel("Prix en dollars américains");

            FormsPlot1.Refresh();
        }
    }
}
