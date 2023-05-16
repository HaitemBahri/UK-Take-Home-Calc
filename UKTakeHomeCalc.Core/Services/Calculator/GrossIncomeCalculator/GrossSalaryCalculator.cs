using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem;

namespace UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator
{
    public class GrossSalaryCalculator : ICalculator
    {
        private List<IIncomeItem> _incomeItems;

        public GrossSalaryCalculator(List<IIncomeItem> incomeItems)
        {
            _incomeItems = incomeItems;
        }

        public MonetaryValue Calculate(MonetaryValue value)
        {
            throw new NotImplementedException();
        }
    }
}
