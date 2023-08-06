using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies
{
    public abstract class CalculationStrategy : ICalculationStrategy
    {
        protected abstract string Name { get; }
        protected ITakeHomeSummaryCompositeBuilder TakeHomeSummaryCompositeBuilder { get; }

        protected CalculationStrategy(ITakeHomeSummaryCompositeBuilder takeHomeSummaryCompositeBuilder)
        {
            if (takeHomeSummaryCompositeBuilder is null)
                throw new ArgumentNullException(nameof(takeHomeSummaryCompositeBuilder));

            TakeHomeSummaryCompositeBuilder = takeHomeSummaryCompositeBuilder;
        }

        public abstract ITakeHomeSummaryItem CreateTakeHomeSummaryItem(ITakeHomeSummaryComposite takeHomeSummary);
    }
}
