using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies
{
    public abstract class DeductableStrategy : CalculationStrategy
    {
        protected abstract List<TieredValueRule> Rules { get; }
        protected IQualifyingIncomeCalculationService QualifyingIncomeCalculationService { get; }
        protected ITieredValueCalculator TieredValueCalculator { get; }
        protected MonetaryValue FreeAllowance { get; }

        public DeductableStrategy(IQualifyingIncomeCalculationService qualifyingIncomeCalculationService,
            ITieredValueCalculator tieredValueCalculator,
            ITakeHomeSummaryCompositeBuilder takeHomeSummaryCompositeBuilder,
            MonetaryValue freeAllowance) :
            base(takeHomeSummaryCompositeBuilder)
        {
            if (qualifyingIncomeCalculationService is null || tieredValueCalculator is null)
                throw new ArgumentNullException();

            if(freeAllowance < 0m.Weekly())
                throw new ArgumentOutOfRangeException();

            QualifyingIncomeCalculationService = qualifyingIncomeCalculationService;
            TieredValueCalculator = tieredValueCalculator;
            FreeAllowance = freeAllowance;
        }

        public override ITakeHomeSummaryItem CreateTakeHomeSummaryItem(ITakeHomeSummaryComposite takeHomeSummery)
        {
            MonetaryValue qualifyingIncome = QualifyingIncomeCalculationService.CalculateQualifyingIncome(takeHomeSummery.GetTotal(), FreeAllowance);

            IEnumerable<TieredValueResult> tieredValueResults = TieredValueCalculator.CalculateTieredValueResults(qualifyingIncome, Rules);

            ITakeHomeSummaryComposite takeHomeSummaryComposite = TakeHomeSummaryCompositeBuilder.Add(tieredValueResults.ToArray()).Build();

            return takeHomeSummaryComposite;
        }
    }
}
