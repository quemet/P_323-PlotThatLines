using System;
using System.Collections.Generic;

namespace P_323_PlotThatLines
{
    interface IForm
    {
        /// <summary>
        /// Draw a graph into the label in the Forms
        /// </summary>
        /// <param name="close_currency">a list of float of the close of every day</param>
        /// <param name="date">first date of the gaph</param>
        /// <param name="currency">currency of the graph</param>
        /// <returns>return an object signal to draw the graph</returns>
        ScottPlot.Plottables.Signal DrawGraph(List<float> close_currency, double date, string currency);
        /// <summary>
        /// Read a file csv and return a list of currency
        /// </summary>
        /// <param name="path">Path to the file csv</param>
        /// <param name="currency">Currency in the file csv</param>
        /// <returns>return a list of Currency from the file</returns>
        List<Currency> ReadPath(string path, string currency);

        /// <summary>
        /// Function to refresh the list currency with startDate and endDate
        /// </summary>
        /// <param name="currency">list of all the currency</param>
        /// <param name="startDate">the date of the start of the graph</param>
        /// <param name="endDate">the date of the end of the graph</param>
        /// <returns>return a list of currency within the two date</returns>
        List<Currency> RefreshDate(List<Currency> currency, DateTime startDate, DateTime endDate);
    }
}
