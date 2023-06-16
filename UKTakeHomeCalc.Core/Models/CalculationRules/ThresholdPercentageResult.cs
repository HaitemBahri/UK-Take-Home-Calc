using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Models.CalculationRules
{
    public class ThresholdPercentageResult
    {
        public ThresholdPercentageRule Rule { get; }
        public MonetaryValue Result { get; }

        public ThresholdPercentageResult(ThresholdPercentageRule rule, MonetaryValue result)
        {
            Rule = rule;
            Result = result;
        }
    }
}
