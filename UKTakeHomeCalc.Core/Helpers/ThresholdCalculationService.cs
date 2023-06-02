using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Models.CalculationRules;

namespace UKTakeHomeCalc.Core.Helpers
{
    public static class ThresholdCalculationService
    {
        public static MonetaryValue CalcValueBetweenThresholds(MonetaryValue value, MonetaryValue lowerThreshold, MonetaryValue upperThreshold)
        {
            if (value > upperThreshold)
                return upperThreshold - lowerThreshold;

            if (value < lowerThreshold)
                return 0m;

            return value - lowerThreshold;
        }

        public static CalculationRuleResult CalcValuesUsingRules(MonetaryValue value, CalculationRule calculationRule)
        {
            var results = new CalculationRuleResult();

            foreach (var rule in calculationRule.Rules)
            {
                var valueInBetween = CalcValueBetweenThresholds(value, rule.FromValue, rule.ToValue);
                var taxOnValueInBetween = valueInBetween * rule.Percentage;
                results.AddResult(rule.Percentage, taxOnValueInBetween);
            }

            return results;
        }
    }
}
