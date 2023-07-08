using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TieredValueCalculators;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategy
{
    public class EnglandTaxStrategy : ICalculationStrategy
    {
        private readonly string _name;
        private readonly List<TieredValueRule> _rules;
        private readonly IQualifyingIncomeCalculationService _qualifyingIncomeCalculationService;
        private readonly ITieredValueCalculator _tieredValueCalculator;

        public EnglandTaxStrategy(string name, IQualifyingIncomeCalculationService qualifyingIncomeCalculationService,
            ITieredValueCalculator tieredValueCalculator)
        {
            _name = name;
            _qualifyingIncomeCalculationService = qualifyingIncomeCalculationService;
            _tieredValueCalculator = tieredValueCalculator; 
            _rules = new List<TieredValueRule>()
            {
                new TieredValueRule(0m.Annually(), 37700m.Annually(), 0.2m),
                new TieredValueRule(37700m.Annually(), 125140m.Annually(), 0.4m),
                new TieredValueRule(125140m.Annually(), 1000000m.Annually(), 0.45m),
            };
        }
        public ITakeHomeSummaryItem CreateSalaryItem(ITakeHomeSummaryComposite takeHomeSummery)
        {
            var qualifyingIncome = _qualifyingIncomeCalculationService.CalculateQualifyingIncome(takeHomeSummery.GetTotal());

            var tieredValueResults = _tieredValueCalculator.CalculateTieredValueResults(qualifyingIncome, _rules);

            var salaryItemNode = new TakeHomeSummaryComposite(_name);
            foreach (var result in tieredValueResults)
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
