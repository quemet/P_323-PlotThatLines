using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_FUN_PlotThatLines
{
    internal class DefaultDataHandler : IDataHandler
    {
        public DateTime ComputeDate(DateTime t1, DateTime t2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Currency> FilterData(IEnumerable<object> source)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Currency> GetData(Currency currency)
        {
            throw new NotImplementedException();
        }
    }
}
