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
        private readonly IFreeAllowance _freeAllowance;

        public NationalInsuranceQualifyingSalaryCalculationService(IFreeAllowance freeAllowance)
        {
            _freeAllowance = freeAllowance;
        }
        public MonetaryValue CalculateQualifyingSalary(MonetaryValue salary)
        {
            var freeAllowance = _freeAllowance.GetFreeAllowance();

            var qualifyingSalary = salary - freeAllowance;

            if (qualifyingSalary < 0)
                qualifyingSalary = 0;

            return qualifyingSalary;
        }
    }
}
