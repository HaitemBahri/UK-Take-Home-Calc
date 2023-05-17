using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem;

namespace UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator
{
    public class GrossSalaryCalculator : ICalculator
    {
        private List<IIncomeItem> _incomeItems;
        private ISalaryItemComposite _nameNode;

        public GrossSalaryCalculator(ISalaryItemComposite nameNode, List<IIncomeItem> incomeItems)
        {
            _incomeItems = incomeItems;
            _nameNode = nameNode;
        }

        public void AddSalaryItemToSalary(ISalaryItemComposite salary)
        {
            foreach (var incomeItem in _incomeItems)
            {
                _nameNode.AddValue(incomeItem.GetValue());
            }

            salary.AddValue(_nameNode);
        }
    }
}
