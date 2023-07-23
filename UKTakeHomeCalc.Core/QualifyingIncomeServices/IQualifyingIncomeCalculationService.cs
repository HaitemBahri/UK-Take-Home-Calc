using UKTakeHomeCalc.Core.FreeAllowances;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingIncomeServices
{
    public interface IQualifyingIncomeCalculationService
    {
        public MonetaryValue CalculateQualifyingIncome(MonetaryValue income, MonetaryValue freeAllowance);
    }
}