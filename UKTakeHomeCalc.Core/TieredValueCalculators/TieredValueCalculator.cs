using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public class TieredValueCalculator : ITieredValueCalculator
    {
        public MonetaryValue CalculateValueBetweenThresholds(MonetaryValue value,
            MonetaryValue lowerThreshold, MonetaryValue upperThreshold)
        {
            if (value > upperThreshold)
                return upperThreshold - lowerThreshold;

            if (value < lowerThreshold)
                return 0m;

            return value - lowerThreshold;
        }

        public IEnumerable<TieredValueResult> CalculateTieredValueResults(MonetaryValue value,
            IEnumerable<TieredValueRule> rules)
        {
            var results = new List<TieredValueResult>();

            foreach (var rule in rules)
            {
                var valueBetweenThresholds = CalculateValueBetweenThresholds(value, rule.FromValue, rule.ToValue);

                var thresholdPercentageResult = valueBetweenThresholds * rule.Percentage;

                results.Add(new TieredValueResult(rule, thresholdPercentageResult));
            }

            return results;
        }
    }
}
