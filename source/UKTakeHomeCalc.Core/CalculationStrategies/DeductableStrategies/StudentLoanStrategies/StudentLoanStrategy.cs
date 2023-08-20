using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.StudentLoanStrategies
{
    public abstract class StudentLoanStrategy : DeductableStrategy
    {
        public StudentLoanStrategy(IQualifyingIncomeCalculationService qualifyingIncomeCalculationService,
            ITieredValueCalculator tieredValueCalculator,
            ITakeHomeSummaryCompositeBuilder takeHomeSummaryCompositeBuilder,
            MonetaryValue freeAllowance) :
            base(qualifyingIncomeCalculationService, tieredValueCalculator, takeHomeSummaryCompositeBuilder, freeAllowance)
        {

        }
    }
}
