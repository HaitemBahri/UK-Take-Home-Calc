using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.CalculationStrategies
{
    public interface ICalculationStrategy
    {
        public ISalaryItem CreateSalaryItem(ISalaryItemNode takeHomeSummery);
    }
}