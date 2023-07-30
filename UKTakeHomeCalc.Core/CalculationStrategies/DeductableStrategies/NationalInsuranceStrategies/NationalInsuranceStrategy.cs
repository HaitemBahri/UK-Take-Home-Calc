using UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.NationalInsuranceStrategies
{
    public abstract class NationalInsuranceStrategy : DeductableStrategy
    {
        public NationalInsuranceStrategy(IQualifyingIncomeCalculationService qualifyingIncomeCalculationService,
            ITieredValueCalculator tieredValueCalculator,
            ITakeHomeSummaryCompositeBuilder takeHomeSummaryCompositeBuilder,
            MonetaryValue freeAllowance) :
            base(qualifyingIncomeCalculationService, tieredValueCalculator, takeHomeSummaryCompositeBuilder, freeAllowance)
        {

        }
    }
}
