namespace P_FUN_PlotThatLines
{
    internal interface IDataHandler
    {

        /// <summary>
        /// Function to refresh the list currency with startDate and endDate
        /// </summary>
        /// <param name="currency">list of all the currency</param>
        /// <param name="startDate">the date of the start of the graph</param>
        /// <param name="endDate">the date of the end of the graph</param>
        /// <returns>return a list of currency within the two date</returns>
        public List<Currency> RefreshDate(List<Currency> currency, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Redraw the new graph with the new date
        /// </summary>
        /// <param name="sender">Contains a reference of the control/object</param>
        /// <param name="e">Contains all the event data </param>
        /// <returns>a list of three list of currency</returns>
        public List<List<Currency>> ChangeDate(object sender, EventArgs e);

        /// <summary>
        /// Read a file csv and return a list of currency
        /// </summary>
        /// <param name="path">Path to the file csv</param>
        /// <param name="currency">Currency in the file csv</param>
        /// <returns>return a list of Currency from the file</returns>
        public List<Currency> ReadPath(string path, string currency);

        /// <summary>
        /// Return the date with the good format
        /// </summary>
        /// <param name="c">list of a currency</param>
        /// <param name="index">index to return it</param>
        /// <returns>return a date of the format of OA</returns>
        public double ReturnCorrectFormatDate(List<Currency> c, int index);
    }
}
