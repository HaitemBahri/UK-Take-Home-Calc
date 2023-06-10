using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingSalaryServices.QualifyingSalaryCalculationService
{
    public class StandardNationalInsuranceFreeAllowance : IFreeAllowance
    {
        private readonly MonetaryValue _standardNationalInsuranceFreeAllowance = 242m.Weekly();
        public MonetaryValue GetFreeAllowance()
        {
            return _standardNationalInsuranceFreeAllowance;
        }
    }
}
