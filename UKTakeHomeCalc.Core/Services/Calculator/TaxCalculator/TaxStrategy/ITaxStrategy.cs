using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxStrategy
{
    public interface ITaxStrategy
    {
        public ISalaryItem CreateTaxSalaryItem(ISalaryItem salary);
    }
}
