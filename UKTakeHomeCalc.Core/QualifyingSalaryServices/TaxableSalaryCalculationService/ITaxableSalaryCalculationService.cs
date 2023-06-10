using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingSalaryServices.TaxableSalaryCalculationService
{
    public interface ITaxableSalaryCalculationService
    {
        public MonetaryValue CalculateTaxableSalary(MonetaryValue salary);
    }
}
