using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Helpers.CalculationRules
{
    public class CalculationRule
    {
        private List<CalculationRuleValues> _rules;
        public List<CalculationRuleValues> Rules { get => _rules; }

        public CalculationRule()
        {
            _rules = new List<CalculationRuleValues>();
        }

        public void AddRule(MonetaryValue fromValue, MonetaryValue toValue, decimal percentage)
        {
            _rules.Add(new CalculationRuleValues(fromValue, toValue, percentage));
        }
    }
}
