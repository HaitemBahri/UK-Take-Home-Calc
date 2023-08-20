using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public class TieredValueRulesBuilder : ITieredValueRulesBuilder
    {
        private readonly List<TieredValueRule> _rules = new List<TieredValueRule>();
        public ITieredValueRulesBuilder Add(MonetaryValue fromValue, MonetaryValue toValue, decimal percentage)
        {
            _rules.Add(new TieredValueRule(fromValue, toValue, percentage));

            return this;
        }

        public List<TieredValueRule> Build()
        {
            return _rules;
        }
    }
}
