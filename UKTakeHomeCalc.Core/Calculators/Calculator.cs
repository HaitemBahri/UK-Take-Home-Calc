using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.Calculators
{
    public class Calculator : ICalculator
    {
        private string _name;
        private readonly ICalculationStrategy[] _calculationStrategies;
        private readonly ITakeHomeSummaryCompositeBuilder _takeHomeSummaryBuilder;

        public Calculator(string name, params ICalculationStrategy[] calculationStrategies)
        {
            _name = name;
            _calculationStrategies = calculationStrategies;
            _takeHomeSummaryBuilder = new TakeHomeSummaryCompositeBuilder(name);
        }

        public ITakeHomeSummaryComposite CreateTakeHomeSummaryComposite(ITakeHomeSummaryComposite takeHomeSummary)
        {
            foreach (var calculationStrategy in _calculationStrategies)
            {
                var takeHomeSummaryItem = calculationStrategy.CreateTakeHomeSummaryItem(takeHomeSummary);
                _takeHomeSummaryBuilder.Add(takeHomeSummaryItem);
            }

            return _takeHomeSummaryBuilder.Build();
        }
    }
}
