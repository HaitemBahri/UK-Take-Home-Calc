using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.PensionCalculator.PensionStrategy;

namespace UKTakeHomeCalc.Core.Services.Calculator.PensionCalculator
{
    public class PensionCalculator : ICalculator
    {
        private List<IPensionStrategy> _pensionStrategies;
        private ISalaryItemNode _nameNode;

        public PensionCalculator(ISalaryItemNode nameNode, List<IPensionStrategy> pensionStrategyies)
        {
            _pensionStrategies = pensionStrategyies;
            _nameNode = nameNode;
        }

        public ISalaryItemNode CreateSalaryItemNode(ISalaryItemNode salary)
        {
            foreach (var pensionStrategy in _pensionStrategies)
            {
                _nameNode.AddValue(pensionStrategy.CreatePensionSalaryItem(salary));
            }

            return _nameNode;
        }
    }
}
