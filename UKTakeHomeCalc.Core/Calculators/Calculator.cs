using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.Calculators
{
    public class Calculator : ICalculator
    {
        private string _name;
        private readonly ICalculationStrategy[] _calculationStrategies;
        private readonly ITakeHomeSummaryCompositeBuilder _takeHomeSummaryBuilder;

        public Calculator(string name, params ICalculationStrategy[] calculationStrategies)
        {
            if (calculationStrategies is null || calculationStrategies.Contains(null))
                throw new ArgumentNullException(nameof(calculationStrategies), "The variable is either null or contains a null value.");

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
