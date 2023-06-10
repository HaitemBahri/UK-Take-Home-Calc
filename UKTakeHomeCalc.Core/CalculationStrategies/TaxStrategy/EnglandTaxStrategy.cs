using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models.CalculationRules;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.QualifyingSalaryServices.TaxableSalaryCalculationService;

namespace UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategy
{
    public class EnglandTaxStrategy : ICalculationStrategy
    {
        private readonly string _name;
        private readonly CalculationRule _taxCalculationRule = new CalculationRule();
        private readonly ITaxableSalaryCalculationService _taxableSalaryCalculationService;

        public EnglandTaxStrategy(string name, ITaxableSalaryCalculationService taxableSalaryCalculationService)
        {
            _name = name;
            _taxableSalaryCalculationService = taxableSalaryCalculationService;

            _taxCalculationRule.AddRule(0m.Annually(), 37700m.Annually(), 0.2m);
            _taxCalculationRule.AddRule(37700m.Annually(), 125140m.Annually(), 0.4m);
            _taxCalculationRule.AddRule(125140m.Annually(), 1000000m.Annually(), 0.45m);
        }
        public ISalaryItem CreateSalaryItem(ISalaryItemNode takeHomeSummery)
        {
            var taxableSalary = _taxableSalaryCalculationService.CalculateTaxableSalary(takeHomeSummery.GetTotal());

            var taxResults = ThresholdCalculationService.CalcValuesUsingRules(taxableSalary, _taxCalculationRule);

            var salaryItemNode = new SalaryItemNode(_name);
            foreach (var taxResult in taxResults.Results)
            {
                if (taxResult.Value == 0)
                    continue;

                var taxPercentage = taxResult.Key;
                salaryItemNode.AddValue(new SalaryItem($"Tax @ %{taxPercentage * 100:G29}", taxResult.Value * -1));
            }

            return salaryItemNode;
        }


    }
}
