using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculationStrategies.IncomeStrategies
{
    public class BasicIncomeStrategy : IncomeStrategy
    {
        protected override string Name { get; }
        protected override MonetaryValue MonetaryValue { get; }

        public BasicIncomeStrategy(string name, decimal value, Frequency frequency) : 
            base(new TakeHomeSummaryCompositeBuilder(name))
        {
            MonetaryValue = value.Every(frequency);
            Name = name;
        }

        public override ITakeHomeSummaryItem CreateTakeHomeSummaryItem(ITakeHomeSummaryComposite takeHomeSummary)
        {
            return new TakeHomeSummaryItem(Name, MonetaryValue);
        }
    }
}
