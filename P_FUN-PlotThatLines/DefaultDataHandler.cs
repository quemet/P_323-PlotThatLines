using System.Runtime.CompilerServices;

namespace P_FUN_PlotThatLines
{
    public class DefaultDataHandler : IDataHandler
    {
        // list of all the data for the bitcoin
        public List<Currency> bitcoin = new List<Currency>();

        // list of all the data for the ethereum
        public List<Currency> ethereum = new List<Currency>();

        // list of all the data for solana
        public List<Currency> solana = new List<Currency>();

        // list of all the close of bitcoin
        public List<float> close_bitcoin = new List<float>();

        // list of all the close of ethereum
        public List<float> close_ethereum = new List<float>();

        // list of all the close of solana
        public List<float> close_solana = new List<float>();

        // a datetimepicker for the start of the graph
        public DateTimePicker startPicker = new DateTimePicker();

        // a datetimepicker for the end of the graph
        public DateTimePicker endPicker = new DateTimePicker();

        // a button to submit the new date
        Button b_submit = new Button();

        public DefaultDataHandler InitializeObject(DefaultDataHandler d)
        {
            d.bitcoin = ReadPath("../../bitcoin.csv", "bitcoin");
            d.ethereum = ReadPath("../../ethereum.csv", "ethereum");
            d.solana = ReadPath("../../solana.csv", "solana");
            d.close_bitcoin = bitcoin.Select(b => b._close).ToList();
            d.close_ethereum = ethereum.Select(e => e._close).ToList();
            d.close_solana = solana.Select(s => s._close).ToList();
            d.startPicker.Value = new DateTime(2020, 1, 1);
            d.endPicker.Value = new DateTime(2021, 1, 1);

            return d;
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
        /// <returns>a list of three list of currency</returns>
        public List<List<Currency>> ChangeDate(object sender, EventArgs e)
        {
            DateTime start_date = startPicker.Value;
            DateTime end_date = endPicker.Value;

            List<Currency> bit = RefreshDate(bitcoin, start_date, end_date);
            List<Currency> eth = RefreshDate(ethereum, start_date, end_date);
            List<Currency> sln = RefreshDate(solana, start_date, end_date);

            return new List<List<Currency>>() { bit, eth, sln };
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

        /// <summary>
        /// Return the date with the good format
        /// </summary>
        /// <param name="c">list of a currency</param>
        /// <param name="index">index to return it</param>
        /// <returns>return a date of the format of OA</returns>
        public double ReturnCorrectFormatDate(List<Currency> c, int index)
        {
            if (index == 0) return c.First()._date.ToOADate();
            else if (index == c.Count - 1) return c.Last()._date.ToOADate();
            else return c.ElementAt(index)._date.ToOADate();
        }
    }
}
