using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TieredValueCalculators;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategies
{
    public class EnglandTaxStrategy : TaxStrategy
    {
        protected override string Name { get; }
        protected override List<TieredValueRule> Rules { get; }
        public EnglandTaxStrategy(string name, MonetaryValue freeAllowance) :
            base(new IncomeLimitTaxQualifyingIncomeCalculationService(),
                 new TieredValueCalculator(),
                 new TakeHomeSummaryCompositeBuilder(name),
                 freeAllowance)
        {
            Name = name;

            Rules = new List<TieredValueRule>()
            {
                new TieredValueRule(0m.Annually(), 37700m.Annually(), -0.2m),
                new TieredValueRule(37700m.Annually(), 125140m.Annually(), -0.4m),
                new TieredValueRule(125140m.Annually(), 1000000m.Annually(), -0.45m),
            };
        }

        public override ITakeHomeSummaryItem CreateTakeHomeSummaryItem(ITakeHomeSummaryComposite takeHomeSummery)
        {
            var qualifyingIncome = QualifyingSalaryCalculationService.CalculateQualifyingIncome(takeHomeSummery.GetTotal(), FreeAllowance);

            var tieredValueResults = TieredValueCalculator.CalculateTieredValueResults(qualifyingIncome, Rules);

            var takeHomeSummaryComposite = TakeHomeSummaryCompositeBuilder.Add(tieredValueResults.ToArray()).Build();

            return takeHomeSummaryComposite;
        }
    }
}
