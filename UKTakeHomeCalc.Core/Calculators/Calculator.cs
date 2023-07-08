using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.Calculators
{
    public class Calculator
    {
        private ITakeHomeSummaryComposite _calculatorNode;
        private readonly List<ICalculationStrategy> _calculationStrategies;

        public Calculator(ITakeHomeSummaryComposite calculatorNode, List<ICalculationStrategy> calculationStrategies)
        {
            _calculatorNode = calculatorNode;
            _calculationStrategies = calculationStrategies;
        }

        public ITakeHomeSummaryComposite CreateSalaryItemNode(ITakeHomeSummaryComposite takeHomeSummery)
        {
            foreach (var calculationStrategy in _calculationStrategies)
            {
                var salaryItem = calculationStrategy.CreateSalaryItem(takeHomeSummery);
                _calculatorNode.AddValue(salaryItem);
            }

            return _calculatorNode;
        }
    }
}
