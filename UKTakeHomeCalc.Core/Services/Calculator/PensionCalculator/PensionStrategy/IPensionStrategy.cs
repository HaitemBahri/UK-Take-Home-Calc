using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator.PensionCalculator.PensionStrategy
{
    public interface IPensionStrategy
    {
        public ISalaryItem CreatePensionSalaryItem(ISalaryItem salary);
    }
}