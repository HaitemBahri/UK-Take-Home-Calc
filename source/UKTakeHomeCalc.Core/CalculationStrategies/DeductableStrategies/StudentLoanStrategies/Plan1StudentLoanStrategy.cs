using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.StudentLoanStrategies
{
    public class Plan1StudentLoanStrategy : StudentLoanStrategy
    {
        protected override string Name { get; }
        protected override List<TieredValueRule> Rules { get; }
        public Plan1StudentLoanStrategy(string name) :
            base(new IncomeLimitTaxQualifyingIncomeCalculationService(),
                 new TieredValueCalculator(),
                 new TakeHomeSummaryCompositeBuilder(name),
                 FreeAllowances.StudentLoan.Plan1StudentLoanFreeAllownace)
        {
            Name = name;

            Rules = new List<TieredValueRule>()
            {
                new TieredValueRule(0m.Annually(), 10_000_000m.Annually(), -0.09m),
            };
        }
    }
}
