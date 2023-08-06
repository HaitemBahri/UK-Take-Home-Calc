using UKTakeHomeCalc.Core.Calculators;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculatorsHandlers
{
    public class CalculatorsHandler : ICalculatorsHandler
    {
        private ICalculatorsHandler? _nextCalculatorsHandler = null;
        private ICalculator[] _calculators;

        public CalculatorsHandler(params ICalculator[] calculators)
        {
            if (calculators is null || calculators.Contains(null))
                throw new ArgumentNullException(nameof(calculators), "The variable is either null or contains a null value.");

            _calculators = calculators;
        }

        public void SetNext(ICalculatorsHandler nextCalculatorsHandler)
        {
            _nextCalculatorsHandler = nextCalculatorsHandler;
        }

        public void Handle(ITakeHomeSummaryComposite takeHomeSummary)
        {
            var takeHomeSummaryItems = new List<ITakeHomeSummaryItem>();

            foreach (var calculator in _calculators)
            {
                var takeHomeSummaryComposite = calculator.CreateTakeHomeSummaryComposite(takeHomeSummary);
                takeHomeSummaryItems.Add(takeHomeSummaryComposite);
            }

            foreach (var takeHomeSummaryItem in takeHomeSummaryItems)
            {
                takeHomeSummary.AddValue(takeHomeSummaryItem);
            }

            if (_nextCalculatorsHandler != null)
                _nextCalculatorsHandler.Handle(takeHomeSummary);
        }
    }
}
