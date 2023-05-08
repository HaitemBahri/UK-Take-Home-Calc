using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Core.Calculator
{
    public interface ICalculator
    {
        public MonetaryValue Calculate(MonetaryValue value);
    }
}