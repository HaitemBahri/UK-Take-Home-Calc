using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem
{
    public interface IIncomeItem
    {
        public ISalaryItem CreateIncomeSalaryItem();
    }
}