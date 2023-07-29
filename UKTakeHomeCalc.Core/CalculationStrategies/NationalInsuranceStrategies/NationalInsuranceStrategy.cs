using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.NationalInsuranceStrategy
{
    public abstract class NationalInsuranceStrategy : CalculationStrategy
    {
        protected abstract List<TieredValueRule> Rules { get; }
        protected IQualifyingIncomeCalculationService QualifyingSalaryCalculationService { get; }
        protected ITieredValueCalculator TieredValueCalculator { get; }
        protected MonetaryValue FreeAllowance { get; }

        public NationalInsuranceStrategy(IQualifyingIncomeCalculationService qualifyingSalaryCalculationService,
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
