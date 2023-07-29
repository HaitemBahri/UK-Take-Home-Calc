using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.TakeHomeSummaryItems
{
    public class TakeHomeSummaryCompositeBuilder : ITakeHomeSummaryCompositeBuilder
    {
        private readonly ITakeHomeSummaryComposite _composite;

        public TakeHomeSummaryCompositeBuilder(string name)
        {
            _composite = new TakeHomeSummaryComposite(name);
        }

        public ITakeHomeSummaryCompositeBuilder Add(string itemName, MonetaryValue itemValue)
        {
            _composite.AddValue(new TakeHomeSummaryItem(itemName, itemValue));

            return this;
        }

        public ITakeHomeSummaryCompositeBuilder Add(params ITakeHomeSummaryItem[] composites)
        {
            foreach (var composite in composites)
            {
                _composite.AddValue(composite);
            }

            return this;
        }

        public ITakeHomeSummaryCompositeBuilder Add(params TieredValueResult[] results)
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
