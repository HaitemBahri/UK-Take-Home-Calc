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
    public class ClassOneNationalInsuranceStrategy : NationalInsuranceStrategy
    {
        protected override string Name { get; }
        protected override List<TieredValueRule> Rules { get; }

        public ClassOneNationalInsuranceStrategy(string name, MonetaryValue freeAllowance) :
            base(new StandardQualifyingIncomeCalculationService(),
                 new TieredValueCalculator(),
                 new TakeHomeSummaryCompositeBuilder(name),
                 freeAllowance)
        {
            Name = name;

            Rules = new List<TieredValueRule>()
            {
                new TieredValueRule(0m.Weekly(), (967m - 242m).Weekly(), -0.12m),
                new TieredValueRule((967m - 242m).Weekly(), 10000000m.Weekly(), -0.02m),
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
