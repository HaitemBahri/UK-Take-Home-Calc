using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculationStrategies.IncomeStrategies
{
    public class HourlyRateIncomeStrategy : IncomeStrategy
    {
        protected override string Name { get; }
        protected override MonetaryValue MonetaryValue { get; }

        public HourlyRateIncomeStrategy(string name, decimal hourlyRate, decimal hours, Frequency frequency) :
            base(new TakeHomeSummaryCompositeBuilder(name))
        {
            if (hourlyRate < 0 || hours < 0)
                throw new ArgumentOutOfRangeException(nameof(hourlyRate));

            MonetaryValue = (hourlyRate * hours).Every(frequency);
            Name = name;
        }

        public override ITakeHomeSummaryItem CreateTakeHomeSummaryItem(ITakeHomeSummaryComposite takeHomeSummary)
        {
            return new TakeHomeSummaryItem(Name, MonetaryValue);
        }
    }
}
