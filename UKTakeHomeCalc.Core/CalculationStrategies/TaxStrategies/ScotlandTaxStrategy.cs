using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategy
{
    public class ScotlandTaxStrategy : TaxStrategy
    {
        protected override string Name { get; }
        protected override List<TieredValueRule> Rules { get; }
        public ScotlandTaxStrategy(string name, MonetaryValue freeAllowance) :
            base(new IncomeLimitTaxQualifyingIncomeCalculationService(),
                 new TieredValueCalculator(),
                 new TakeHomeSummaryCompositeBuilder(name),
                 freeAllowance)
        {
            Name = name;

            Rules = new List<TieredValueRule>()
            {
                new TieredValueRule(0m.Annually(), 2162m.Annually(), -0.19m),
                new TieredValueRule(2162m.Annually(), 13118m.Annually(), -0.2m),
                new TieredValueRule(13118m.Annually(), 31092m.Annually(), -0.21m),
                new TieredValueRule(31092m.Annually(), 125140m.Annually(), -0.42m),
                new TieredValueRule(125140m.Annually(), 100000000m.Annually(), -0.47m),
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
