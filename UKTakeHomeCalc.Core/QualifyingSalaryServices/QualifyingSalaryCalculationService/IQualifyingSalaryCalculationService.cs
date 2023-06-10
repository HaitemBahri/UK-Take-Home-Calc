using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingSalaryServices.QualifyingSalaryCalculationService
{
    public interface IQualifyingSalaryCalculationService
    {
        public MonetaryValue CalculateQualifyingSalary(MonetaryValue salary);
    }
}