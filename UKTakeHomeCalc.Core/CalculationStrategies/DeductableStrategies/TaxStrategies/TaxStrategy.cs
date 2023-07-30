using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;
using UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies;

namespace UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.TaxStrategies
{
    public abstract class TaxStrategy : DeductableStrategy
    {
        public TaxStrategy(IQualifyingIncomeCalculationService qualifyingIncomeCalculationService,
            ITieredValueCalculator tieredValueCalculator,
            ITakeHomeSummaryCompositeBuilder takeHomeSummaryCompositeBuilder,
            MonetaryValue freeAllowance) :
            base(qualifyingIncomeCalculationService, tieredValueCalculator, takeHomeSummaryCompositeBuilder, freeAllowance)
        {

        }

    }
}
