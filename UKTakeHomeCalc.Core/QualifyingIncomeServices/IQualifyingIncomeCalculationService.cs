using UKTakeHomeCalc.Core.FreeAllowance;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingIncomeServices
{
    public interface IQualifyingIncomeCalculationService
    {
        public MonetaryValue CalculateQualifyingIncome(MonetaryValue income);
    }
}