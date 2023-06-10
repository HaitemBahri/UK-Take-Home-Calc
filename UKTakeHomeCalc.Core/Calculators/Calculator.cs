using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Calculators
{
    public class Calculator
    {
        private ISalaryItemNode _calculatorNode;
        private readonly List<ICalculationStrategy> _calculationStrategies;

        public Calculator(ISalaryItemNode calculatorNode, List<ICalculationStrategy> calculationStrategies)
        {
            _calculatorNode = calculatorNode;
            _calculationStrategies = calculationStrategies;
        }

        public ISalaryItemNode CreateSalaryItemNode(ISalaryItemNode takeHomeSummery)
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
