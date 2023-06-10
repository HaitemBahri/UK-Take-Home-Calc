using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Calculators
{
    public interface ICalculator
    {
        public ISalaryItemNode CreateSalaryItemNode(ISalaryItemNode takeHomeSummery);
    }
}