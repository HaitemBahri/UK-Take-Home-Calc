using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Models.CalculationRules;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxableSalaryCalculationService;

namespace UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxStrategy
{
    public class ScotlandTaxStrategy : ITaxStrategy
    {
        private readonly string _name;
        private readonly CalculationRule _taxCalculationRule = new CalculationRule();
        private readonly ITaxableSalaryCalculationService _taxableSalaryCalculationService;

        public ScotlandTaxStrategy(string name, ITaxableSalaryCalculationService taxableSalaryCalculationService)
        {
            _name = name;
            _taxableSalaryCalculationService = taxableSalaryCalculationService;

            _taxCalculationRule.AddRule(0m.Annually(), 2162m.Annually(), 0.19m);
            _taxCalculationRule.AddRule(2162m.Annually(), 13118m.Annually(), 0.2m);
            _taxCalculationRule.AddRule(13118m.Annually(), 31092m.Annually(), 0.21m);
            _taxCalculationRule.AddRule(31092m.Annually(), 125140m.Annually(), 0.42m);
            _taxCalculationRule.AddRule(125140m.Annually(), 100000000m.Annually(), 0.47m);
        }
        public ISalaryItem CreateTaxSalaryItem(ISalaryItem salary)
        {
            var taxableSalary = _taxableSalaryCalculationService.CalculateTaxableSalary(salary.GetTotal());

            var taxResults = ThresholdCalculationService.CalcValuesUsingRules(taxableSalary, _taxCalculationRule);

            var salaryItemNode = new SalaryItemNode(_name);
            foreach (var taxResult in taxResults.Results)
            {
                if(taxResult.Value == 0)
                    continue;

                var taxPercentage = taxResult.Key;
                salaryItemNode.AddValue(new SalaryItem($"Tax @ %{taxPercentage*100:G29}", taxResult.Value * (-1)));
            }

            return salaryItemNode;
        }
    }
}
