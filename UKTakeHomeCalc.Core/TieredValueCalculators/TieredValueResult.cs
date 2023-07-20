using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public struct TieredValueResult
    {
        public TieredValueRule Rule { get; }
        public decimal RulePercentage { get => Rule.Percentage; }
        public MonetaryValue Result { get; }

        public TieredValueResult(TieredValueRule rule, MonetaryValue result)
        {
            Rule = rule;
            Result = result;
        }
    }
}
