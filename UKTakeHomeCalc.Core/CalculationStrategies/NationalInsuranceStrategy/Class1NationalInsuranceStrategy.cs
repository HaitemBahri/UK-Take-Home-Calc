using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Models.CalculationRules;
using UKTakeHomeCalc.Core.QualifyingSalaryServices.QualifyingSalaryCalculationService;
using UKTakeHomeCalc.Core.QualifyingSalaryServices.TaxableSalaryCalculationService;

namespace UKTakeHomeCalc.Core.CalculationStrategies.NationalInsuranceStrategy
{
    public class Class1NationalInsuranceStrategy : ICalculationStrategy
    {
        private readonly string _name;
        private readonly List<ThresholdPercentageRule> _rules;
        private readonly IQualifyingSalaryCalculationService _qualifyingSalaryCalculationService;
        private readonly IFreeAllowance _freeAllowance;

        public Class1NationalInsuranceStrategy(string name, 
            IQualifyingSalaryCalculationService qualifyingSalaryCalculationService, IFreeAllowance freeAllowance)
        {
            _name = name;
            _qualifyingSalaryCalculationService = qualifyingSalaryCalculationService;
            _freeAllowance = freeAllowance;

            _rules = new List<ThresholdPercentageRule>()
            {
                new ThresholdPercentageRule(0m.Weekly(), (967m - 242m).Annually(), 0.12m),
                new ThresholdPercentageRule((967m - 242m).Annually(), 10000000m.Annually(), 0.02m),
            };
        }

        public ISalaryItem CreateSalaryItem(ISalaryItemNode takeHomeSummery)
        {
            var qualifyingIncome = _qualifyingSalaryCalculationService.CalculateQualifyingSalary(takeHomeSummery.GetTotal(), 
                _freeAllowance);

            var thresholdCalculationService = new ThresholdCalculationService();
            var thresholdPercentageResults = thresholdCalculationService.CalculateThresholdPercentageResult(qualifyingIncome, _rules);

            var salaryItemNode = new SalaryItemNode(_name);
            foreach (var result in thresholdPercentageResults)
            {
                if (result.Result == 0)
                    continue;

                var percentage = result.Rule.Percentage;     //TODO: refactor. (law of demeter violation)

                salaryItemNode.AddValue(new SalaryItem($"NI @ %{percentage * 100:G29}", result.Result * -1));
            }

            return salaryItemNode;

        }
    }
}
