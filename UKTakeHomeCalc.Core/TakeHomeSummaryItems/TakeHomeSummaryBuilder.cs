using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.TakeHomeSummaryItems
{
    public class TakeHomeSummaryBuilder : ITakeHomeSummaryBuilder
    {
        private readonly ITakeHomeSummaryComposite _composite;
        public TakeHomeSummaryBuilder(string name)
        {
            _composite = new TakeHomeSummaryComposite(name);
        }

        public ITakeHomeSummaryBuilder Add(string itemName, MonetaryValue itemValue)
        {
            _composite.AddValue(new TakeHomeSummaryItem(itemName, itemValue));

            return this;
        }

        public ITakeHomeSummaryBuilder Add(params ITakeHomeSummaryComposite[] composites)
        {
            foreach (var composite in composites)
            {
                _composite.AddValue(composite);
            }

            return this;
        }

        public ITakeHomeSummaryBuilder Add(params TieredValueResult[] results)
        {
            foreach (var result in results)
            {
                Add($"@ [%{result.RulePercentage*100:N2}]", result.Result);
            }

            return this;
        }

        public ITakeHomeSummaryComposite Build()
        {
            return _composite;
        }
    }
}
