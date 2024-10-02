using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_FUN_PlotThatLines
{
    internal interface IDataHandler
    {
        IEnumerable<Currency> GetData(Currency currency);
        IEnumerable<Currency> FilterData(IEnumerable<object> source);

        DateTime ComputeDate(DateTime t1,DateTime t2);

        //...
    }
}
