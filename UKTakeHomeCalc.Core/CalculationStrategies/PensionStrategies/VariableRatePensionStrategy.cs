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

namespace UKTakeHomeCalc.Core.CalculationStrategies.PensionStrategy
{
    public class VariableRatePensionStrategy : PensionStrategy
    {
        protected override string Name { get; }
        protected override List<TieredValueRule> Rules { get; }

        public VariableRatePensionStrategy(string name, decimal percentage) :
            base(new StandardQualifyingIncomeCalculationService(),
                 new TieredValueCalculator(),
                 new TakeHomeSummaryCompositeBuilder(name),
                 0m)
        {
            Name = name;

            Rules = new List<TieredValueRule>()
            {
                new TieredValueRule(6240m.Annually(), 50270m.Annually(), -percentage),
            };
        }
        public override ITakeHomeSummaryItem CreateTakeHomeSummaryItem(ITakeHomeSummaryComposite takeHomeSummery)
        {
            var qualifyingIncome = QualifyingSalaryCalculationService.CalculateQualifyingIncome(takeHomeSummery.GetTotal(), FreeAllowance);

            var tieredValueResults = TieredValueCalculator.CalculateTieredValueResults(qualifyingIncome, Rules);

            TakeHomeSummaryCompositeBuilder.Add(tieredValueResults.ToArray());

            var takeHomeSummaryComposite = TakeHomeSummaryCompositeBuilder.Build();

            return takeHomeSummaryComposite;
        }
    }
}
