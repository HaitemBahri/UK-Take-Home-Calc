using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.Calculators
{
    public interface ICalculator
    {
        public ITakeHomeSummaryComposite CreateSalaryItemNode(ITakeHomeSummaryComposite takeHomeSummery);
    }
}