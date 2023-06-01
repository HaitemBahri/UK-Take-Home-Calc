using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Models.CalculationRules;

namespace UKTakeHomeCalc.Core.Helpers
{
    public class BetweenThresholdCalculationService
    {
        public MonetaryValue GetValuesBetweenThresholds(MonetaryValue value, MonetaryValue lowerThreshold, MonetaryValue upperThreshold)
        {
            if (value > upperThreshold)
                return upperThreshold - lowerThreshold;

            if (value < lowerThreshold)
                return 0m;

            return value - lowerThreshold;
        }

        public CalculationRuleResult CalculateValuesUsingCalculationRule(MonetaryValue value, CalculationRule calculationRule)
        {
            var results = new CalculationRuleResult();

            foreach (var rule in calculationRule.Rules)
            {
                var valueInBetween = GetValuesBetweenThresholds(value, rule.FromValue, rule.ToValue);
                var taxOnValueInBetween = valueInBetween * rule.Percentage;
                results.AddResult(rule.Percentage, taxOnValueInBetween);
            }

            return results;
        }
    }
}
