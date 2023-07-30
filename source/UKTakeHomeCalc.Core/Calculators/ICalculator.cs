using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.Calculators
{
    public interface ICalculator
    {
        public ITakeHomeSummaryComposite CreateTakeHomeSummaryComposite(ITakeHomeSummaryComposite takeHomeSummary);
    }
}