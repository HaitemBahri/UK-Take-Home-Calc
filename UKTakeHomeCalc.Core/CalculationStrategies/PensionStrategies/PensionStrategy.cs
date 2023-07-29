using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.PensionStrategies
{
    public abstract class PensionStrategy : CalculationStrategy
    {
        protected abstract List<TieredValueRule> Rules { get; }
        protected IQualifyingIncomeCalculationService QualifyingSalaryCalculationService { get; }
        protected ITieredValueCalculator TieredValueCalculator { get; }
        protected MonetaryValue FreeAllowance { get; }

        public PensionStrategy(IQualifyingIncomeCalculationService qualifyingSalaryCalculationService,
            ITieredValueCalculator tieredValueCalculator,
            ITakeHomeSummaryCompositeBuilder takeHomeSummaryCompositeBuilder,
            MonetaryValue freeAllowance) :
            base(takeHomeSummaryCompositeBuilder)
        {
            QualifyingSalaryCalculationService = qualifyingSalaryCalculationService;
            TieredValueCalculator = tieredValueCalculator;
            FreeAllowance = freeAllowance;
        }
    }
}
