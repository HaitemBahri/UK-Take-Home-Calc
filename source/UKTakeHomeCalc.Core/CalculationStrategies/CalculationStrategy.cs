using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculationStrategies
{
    public abstract class CalculationStrategy : ICalculationStrategy
    {
        protected abstract string Name { get; }
        protected ITakeHomeSummaryCompositeBuilder TakeHomeSummaryCompositeBuilder { get; }

        protected CalculationStrategy(ITakeHomeSummaryCompositeBuilder takeHomeSummaryCompositeBuilder)
        {
            TakeHomeSummaryCompositeBuilder = takeHomeSummaryCompositeBuilder;
        }

        public abstract ITakeHomeSummaryItem CreateTakeHomeSummaryItem(ITakeHomeSummaryComposite takeHomeSummary);
    }
}
