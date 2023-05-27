using System.Collections.Concurrent;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator;

namespace UKTakeHomeCalc.Core.Services.CalculatorsHandler
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
        public void Handle(ISalaryItemNode salary)
        {
            var salaryItemsBag = new ConcurrentBag<ISalaryItem>();

            Parallel.ForEach(_calculators, (calculator) =>
            {
                salaryItemsBag.Add(calculator.CreateSalaryItemNode(salary));
            });

            foreach (var salaryItem in salaryItemsBag)
            {
                salary.AddValue(salaryItem);
            }

            if (_nextCalculatorsHandler != null)
                _nextCalculatorsHandler.Handle(salary);
        }
    }
}
