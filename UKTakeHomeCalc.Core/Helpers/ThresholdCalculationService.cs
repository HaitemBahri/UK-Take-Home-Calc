using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Models.CalculationRules;

namespace UKTakeHomeCalc.Core.Helpers
{
    public class ThresholdCalculationService
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

        public List<ThresholdPercentageResult> CalculateThresholdPercentageResult(MonetaryValue value, 
            List<ThresholdPercentageRule> rules)
        {
            var results = new List<ThresholdPercentageResult>();

            foreach (var rule in rules)
            {
                var valueBetweenThresholds = CalculateValueBetweenThresholds(value, rule.FromValue, rule.ToValue);

                var thresholdPercentageResult = valueBetweenThresholds * rule.Percentage;

                results.Add(new ThresholdPercentageResult(rule, thresholdPercentageResult));
            }

            return results;
        }
    }
}
