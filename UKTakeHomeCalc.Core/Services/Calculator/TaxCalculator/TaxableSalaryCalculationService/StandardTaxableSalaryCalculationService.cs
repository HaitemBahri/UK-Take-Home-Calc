using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxFreeAllowance;

namespace UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxableSalaryCalculationService
{
    public class StandardTaxableSalaryCalculationService : ITaxableSalaryCalculationService
    {
        private readonly ITaxFreeAllowance _taxFreeAllowance;
        public StandardTaxableSalaryCalculationService(ITaxFreeAllowance taxFreeAllowance)
        {
            _taxFreeAllowance = taxFreeAllowance;
        }

        public MonetaryValue CalculateTaxableSalary(MonetaryValue salary)
        {
            var taxFreeAllowance = _taxFreeAllowance.GetTaxFreeAllowance();
            var taxableSalary = salary - taxFreeAllowance;

            if (taxableSalary < 0)
                taxableSalary = 0;

            return taxableSalary;
        }
    }
}
