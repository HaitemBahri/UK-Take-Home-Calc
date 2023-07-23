using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculationStrategies.IncomeStrategies
{
    public abstract class IncomeStrategy : CalculationStrategy
    {
        protected abstract MonetaryValue MonetaryValue { get; }

        protected IncomeStrategy(ITakeHomeSummaryCompositeBuilder takeHomeSummaryCompositeBuilder) : 
            base(takeHomeSummaryCompositeBuilder)
        {

        }
    }
}
