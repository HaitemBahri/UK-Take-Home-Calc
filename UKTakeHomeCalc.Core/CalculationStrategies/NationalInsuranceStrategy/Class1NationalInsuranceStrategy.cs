using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.FreeAllowances;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.NationalInsuranceStrategy
{
    public class Class1NationalInsuranceStrategy : ICalculationStrategy
    {
        private readonly string _name;
        private readonly List<TieredValueRule> _rules;
        private readonly IQualifyingIncomeCalculationService _qualifyingSalaryCalculationService;
        private readonly IFreeAllowance _freeAllowance;

        public Class1NationalInsuranceStrategy(string name, 
            IQualifyingIncomeCalculationService qualifyingSalaryCalculationService, IFreeAllowance freeAllowance)
        {
            _name = name;
            _qualifyingSalaryCalculationService = qualifyingSalaryCalculationService;
            _freeAllowance = freeAllowance;

            _rules = new List<TieredValueRule>()
            {
                new TieredValueRule(0m.Weekly(), (967m - 242m).Annually(), 0.12m),
                new TieredValueRule((967m - 242m).Annually(), 10000000m.Annually(), 0.02m),
            };
        }

        public ITakeHomeSummaryItem CreateSalaryItem(ITakeHomeSummaryComposite takeHomeSummery)
        {
            var qualifyingIncome = _qualifyingSalaryCalculationService.CalculateQualifyingIncome(takeHomeSummery.GetTotal());

            var thresholdCalculationService = new TieredValueCalculator();
            var thresholdPercentageResults = thresholdCalculationService.CalculateTieredValueResults(qualifyingIncome, _rules);

            var salaryItemNode = new TakeHomeSummaryComposite(_name);
            foreach (var result in thresholdPercentageResults)
            {
                if (result.Result == 0)
                    continue;

                var percentage = result.Rule.Percentage;     //TODO: refactor. (law of demeter violation)

                salaryItemNode.AddValue(new TakeHomeSummaryItem($"NI @ %{percentage * 100:G29}", result.Result * -1));
            }

            return salaryItemNode;

        }
    }
}
