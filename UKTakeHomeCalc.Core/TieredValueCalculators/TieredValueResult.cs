using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public struct TieredValueResult
    {
        public TieredValueRule Rule { get; }
        public decimal RulePercentage { get => Rule.Percentage; }
        public MonetaryValue Result { get; private set; }

        public TieredValueResult(TieredValueRule rule, MonetaryValue result)
        {
            Rule = rule;
            Result = result;
        }

        public void Negate()
        {
            Result = -Result;
        }

        public override string ToString()
        {
            return $"Rule = {Rule}, Result = {Result}";
        }
    }
}
