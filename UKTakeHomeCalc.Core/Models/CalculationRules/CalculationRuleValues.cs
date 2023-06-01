using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Models.CalculationRules
{
    public struct CalculationRuleValues
    {
        public CalculationRuleValues(MonetaryValue fromValue, MonetaryValue toValue, decimal percentage)
        {
            FromValue = fromValue;
            ToValue = toValue;
            Percentage = percentage;
        }

        public MonetaryValue FromValue { get; }
        public MonetaryValue ToValue { get; }
        public decimal Percentage { get; }
    }
}
