using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem;

namespace UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator
{
    public class GrossSalaryCalculator : ICalculator
    {
        private List<IIncomeItem> _incomeItems;
        private ISalaryItemNode _nameNode;

        public GrossSalaryCalculator(ISalaryItemNode nameNode, List<IIncomeItem> incomeItems)
        {
            _incomeItems = incomeItems;
            _nameNode = nameNode;
        }

        public ISalaryItemNode CreateSalaryItemNode(ISalaryItemNode salary)
        {
            foreach (var incomeItem in _incomeItems)
            {
                _nameNode.AddValue(incomeItem.CreateSalaryItem());
            }

            return _nameNode;
        }
    }
}
