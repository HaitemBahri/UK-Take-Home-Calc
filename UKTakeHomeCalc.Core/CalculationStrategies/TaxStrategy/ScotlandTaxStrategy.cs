using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategy
{
    public class ScotlandTaxStrategy : ICalculationStrategy
    {
        private readonly string _name;
        private readonly List<TieredValueRule> _rules;
        private readonly IQualifyingIncomeCalculationService _taxableSalaryCalculationService;

        public ScotlandTaxStrategy(string name,
            IQualifyingIncomeCalculationService taxableSalaryCalculationService)
        {
            _name = name;
            _taxableSalaryCalculationService = taxableSalaryCalculationService;

            _rules = new List<TieredValueRule>()
            {
                new TieredValueRule(0m.Annually(), 2162m.Annually(), 0.19m),
                new TieredValueRule(2162m.Annually(), 13118m.Annually(), 0.2m),
                new TieredValueRule(13118m.Annually(), 31092m.Annually(), 0.21m),
                new TieredValueRule(31092m.Annually(), 125140m.Annually(), 0.42m),
                new TieredValueRule(125140m.Annually(), 100000000m.Annually(), 0.47m),
            };
        }
        public ITakeHomeSummaryItem CreateSalaryItem(ITakeHomeSummaryComposite takeHomeSummery)
        {
            var taxableSalary = _taxableSalaryCalculationService.CalculateQualifyingIncome(takeHomeSummery.GetTotal());

            var thresholdCalculationService = new TieredValueCalculator();
            var thresholdPercentageResults = thresholdCalculationService.CalculateTieredValueResults(taxableSalary, _rules);

            var salaryItemNode = new TakeHomeSummaryComposite(_name);
            foreach (var result in thresholdPercentageResults)
            {
                if (result.Result == 0)
                    continue;

                var percentage = result.Rule.Percentage;     //TODO: refactor. (law of demeter violation)

                salaryItemNode.AddValue(new TakeHomeSummaryItem($"Tax @ %{percentage * 100:G29}", result.Result * -1));
            }

            return salaryItemNode;
        }
    }
}
