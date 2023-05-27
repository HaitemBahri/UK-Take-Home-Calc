using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator
{
    public interface ICalculator
    {
        public ISalaryItemNode CreateSalaryItemNode(ISalaryItemNode salary);
    }
}