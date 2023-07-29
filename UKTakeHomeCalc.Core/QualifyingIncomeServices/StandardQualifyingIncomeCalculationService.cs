using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingIncomeServices;

public class StandardQualifyingIncomeCalculationService : IQualifyingIncomeCalculationService
{
    public MonetaryValue CalculateQualifyingIncome(MonetaryValue income, MonetaryValue freeAllowance)
    {
        if (income < 0m.Annually())
            throw new ArgumentOutOfRangeException(nameof(income), "Income value cannot be less than zero.");

        var freeAllowanceValue = freeAllowance;

        var qualifyingIncome = CalculateFinalQualifyingIncome(income, freeAllowanceValue);

        return qualifyingIncome;
    }

    private static MonetaryValue CalculateFinalQualifyingIncome(MonetaryValue income, MonetaryValue freeAllowanceValue)
    {
        var qualifyingSalary = income - freeAllowanceValue;

        if (qualifyingSalary < 0)
            qualifyingSalary = 0;

        return qualifyingSalary;
    }
}
