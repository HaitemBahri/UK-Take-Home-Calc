using System.Collections.Concurrent;
using UKTakeHomeCalc.Core.Calculators;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.CalculatorsHandlers
{
    public class CalculatorsHandler : ICalculatorsHandler
    {
        protected ICalculatorsHandler? _nextCalculatorsHandler = null;
        private List<ICalculator> _calculators;
        public CalculatorsHandler(List<ICalculator> calculators)
        {
            _calculators = calculators;
        }
        public void SetNext(ICalculatorsHandler nextCalculatorsHandler)
        {
            _nextCalculatorsHandler = nextCalculatorsHandler;
        }
        public void Handle(ISalaryItemNode takeHomeSummery)
        {
            var salaryItemsBag = new ConcurrentBag<ISalaryItem>();

            Parallel.ForEach(_calculators, (calculator) =>
            {
                var salaryItem = calculator.CreateSalaryItemNode(takeHomeSummery);
                salaryItemsBag.Add(salaryItem);
            });

            foreach (var salaryItem in salaryItemsBag)
            {
                takeHomeSummery.AddValue(salaryItem);
            }

            if (_nextCalculatorsHandler != null)
                _nextCalculatorsHandler.Handle(takeHomeSummery);
        }
    }
}
