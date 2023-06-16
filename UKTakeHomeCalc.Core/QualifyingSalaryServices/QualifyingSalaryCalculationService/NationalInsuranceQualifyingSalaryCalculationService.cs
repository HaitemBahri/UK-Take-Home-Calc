using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingSalaryServices.QualifyingSalaryCalculationService
{
    public class NationalInsuranceQualifyingSalaryCalculationService : IQualifyingSalaryCalculationService
    {
        public MonetaryValue CalculateQualifyingSalary(MonetaryValue salary, IFreeAllowance freeAllowance)
        {
            var freeAllowanceValue = freeAllowance.GetFreeAllowance();

            var qualifyingSalary = salary - freeAllowanceValue;

            if (qualifyingSalary < 0)
                qualifyingSalary = 0;

            return qualifyingSalary;
        }
    }
}
