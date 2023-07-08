using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public interface ITieredValueCalculator
    {
        public IEnumerable<TieredValueResult> CalculateTieredValueResults(MonetaryValue value, IEnumerable<TieredValueRule> rules);
        public MonetaryValue CalculateValueBetweenThresholds(MonetaryValue value, MonetaryValue lowerThreshold, MonetaryValue upperThreshold);
    }
}