using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public interface ITieredValueRulesBuilder
    {
        public ITieredValueRulesBuilder Add(MonetaryValue fromValue, MonetaryValue toValue, decimal percentage);
        public List<TieredValueRule> Build();
    }
}
