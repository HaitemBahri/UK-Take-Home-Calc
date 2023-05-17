using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator;

namespace UKTakeHomeCalc.Core.Services.CalculatorsHandler
{
    public class CalculatorsHandler : CalculatorsHandlerBase
    {
        private List<ICalculator> _calculators;
        public CalculatorsHandler(List<ICalculator> calculators)
        {
            _calculators = calculators;
        }
        public override void Calculate(MonetaryValue value)
        {
            //TODO: implement logic


            //if (_nextCalculatorsHandler == null)
            //    return;
            //else
            //    _nextCalculatorsHandler.AddSalaryItemToSalary(salaryData);
        }
    }
}
