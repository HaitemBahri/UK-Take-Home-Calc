using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public class TieredValueResult
    {
        public TieredValueRule Rule { get; }
        public MonetaryValue Result { get; }

        public TieredValueResult(TieredValueRule rule, MonetaryValue result)
        {
            Rule = rule;
            Result = result;
        }
    }
}
