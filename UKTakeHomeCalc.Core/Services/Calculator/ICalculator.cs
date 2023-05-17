using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator
{
    public interface ICalculator
    {
        public void AddSalaryItemToSalary(ISalaryItemNode salary);
    }
}