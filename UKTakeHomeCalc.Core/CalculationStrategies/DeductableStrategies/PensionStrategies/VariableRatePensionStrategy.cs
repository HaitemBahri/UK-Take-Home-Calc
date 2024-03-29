﻿using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.PensionStrategies
{
    public class VariableRatePensionStrategy : PensionStrategy
    {
        protected override string Name { get; }
        protected override List<TieredValueRule> Rules { get; }

        public VariableRatePensionStrategy(string name, decimal percentage, MonetaryValue freeAllowance) :
            base(new StandardQualifyingIncomeCalculationService(),
                 new TieredValueCalculator(),
                 new TakeHomeSummaryCompositeBuilder(name),
                 freeAllowance)
        {
            Name = name;

            Rules = new List<TieredValueRule>()
            {
                new TieredValueRule(0m.Annually(), (50270m - 6240m).Annually(), -percentage),
            };
        }
    }
}
