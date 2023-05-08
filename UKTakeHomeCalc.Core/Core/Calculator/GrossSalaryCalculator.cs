using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Core.Income;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Core.Calculator
{
    public class GrossSalaryCalculator : ICalculator
    {
        private List<IIncome> _incomes;

        public GrossSalaryCalculator(List<IIncome> incomes)
        {
            _incomes = incomes;
        }

        public MonetaryValue Calculate(MonetaryValue value)
        {
            throw new NotImplementedException();
        }
    }
}
