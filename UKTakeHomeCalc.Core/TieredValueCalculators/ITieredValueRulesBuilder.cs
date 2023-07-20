using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public interface ITieredValueRulesBuilder
    {
        public ITieredValueRulesBuilder Add(MonetaryValue fromValue, MonetaryValue toValue, decimal percentage);
        public List<TieredValueRule> Build();
    }
}
