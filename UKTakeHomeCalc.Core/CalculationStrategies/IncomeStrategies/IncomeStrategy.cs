using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculationStrategies.IncomeStrategies
{
    public abstract class IncomeStrategy : CalculationStrategy
    {
        protected abstract MonetaryValue MonetaryValue { get; }

        protected IncomeStrategy(ITakeHomeSummaryCompositeBuilder takeHomeSummaryCompositeBuilder) : 
            base(takeHomeSummaryCompositeBuilder)
        {

        }
    }
}
