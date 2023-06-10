using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.FreeAllowances.TaxFreeAllowance;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingSalaryServices.TaxableSalaryCalculationService
{
    public class IncomeLimitTaxableSalaryCalculationService : ITaxableSalaryCalculationService
    {
        private readonly MonetaryValue INCOME_LIMIT = 100000m.Annually();
        private readonly ITaxFreeAllowance _taxFreeAllowance;
        public IncomeLimitTaxableSalaryCalculationService(ITaxFreeAllowance taxFreeAllowance)
        {
            _taxFreeAllowance = taxFreeAllowance;
        }

        public MonetaryValue CalculateTaxableSalary(MonetaryValue salary)
        {
            var initialTaxFreeAllowance = _taxFreeAllowance.GetTaxFreeAllowance();
            var taxFreeAllowanceReduction = salary > INCOME_LIMIT ? (salary - INCOME_LIMIT) / 2 : 0;

            var updatedTaxFreeAllowance = initialTaxFreeAllowance - taxFreeAllowanceReduction;
            if (updatedTaxFreeAllowance < 0)
                updatedTaxFreeAllowance = 0;

            var taxableSalary = salary - updatedTaxFreeAllowance;
            if (taxableSalary < 0)
                taxableSalary = 0;

            return taxableSalary;
        }
    }
}
