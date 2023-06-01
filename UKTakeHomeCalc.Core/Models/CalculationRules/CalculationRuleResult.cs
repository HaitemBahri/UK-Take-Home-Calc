using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Helpers.CalculationRules
{
    public class CalculationRuleResult
    {
        private Dictionary<decimal, MonetaryValue> _results;
        public Dictionary<decimal, MonetaryValue> Results { get => _results; }

        public CalculationRuleResult()
        {
            _results = new Dictionary<decimal, MonetaryValue>();
        }

        public void AddResult(decimal percentage, MonetaryValue result)
        {
            _results.Add(percentage, result);
        }
    }
}
