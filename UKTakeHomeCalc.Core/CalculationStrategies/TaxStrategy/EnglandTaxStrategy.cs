using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models.CalculationRules;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;

namespace UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategy
{
    public class EnglandTaxStrategy : ICalculationStrategy
    {
        private readonly string _name;
        private readonly List<ThresholdPercentageRule> _rules;
        private readonly IQualifyingIncomeCalculationService _taxableSalaryCalculationService;

        public EnglandTaxStrategy(string name, IQualifyingIncomeCalculationService taxableSalaryCalculationService)
        {
            _name = name;
            _taxableSalaryCalculationService = taxableSalaryCalculationService;

            _rules = new List<ThresholdPercentageRule>()
            {
                new ThresholdPercentageRule(0m.Annually(), 37700m.Annually(), 0.2m),
                new ThresholdPercentageRule(37700m.Annually(), 125140m.Annually(), 0.4m),
                new ThresholdPercentageRule(125140m.Annually(), 1000000m.Annually(), 0.45m),
            };
        }
        public ISalaryItem CreateSalaryItem(ISalaryItemNode takeHomeSummery)
        {
            var taxableSalary = _taxableSalaryCalculationService.CalculateQualifyingIncome(takeHomeSummery.GetTotal());

            var thresholdCalculationService = new ThresholdCalculationService();
            var thresholdPercentageResults = thresholdCalculationService.CalculateThresholdPercentageResult(taxableSalary, _rules);

            var salaryItemNode = new SalaryItemNode(_name);
            foreach (var result in thresholdPercentageResults)
            {
                if (result.Result == 0)
                    continue;

                var percentage = result.Rule.Percentage;     //TODO: refactor. (law of demeter violation)

                salaryItemNode.AddValue(new SalaryItem($"Tax @ %{percentage * 100:G29}", result.Result * -1));
            }

            return salaryItemNode;
        }


    }
}
