using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public interface ITieredValueResultsBuilder
    {
        public ITieredValueResultsBuilder Add(TieredValueRule rule, MonetaryValue result);
        public List<TieredValueResult> Build();
    }
}
