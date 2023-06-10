using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingSalaryServices.QualifyingSalaryCalculationService
{
    public interface IFreeAllowance
    {
        public MonetaryValue GetFreeAllowance();
    }
}