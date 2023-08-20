using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingIncomeServices;

public class StandardQualifyingIncomeCalculationService : IQualifyingIncomeCalculationService
{
    public MonetaryValue CalculateQualifyingIncome(MonetaryValue income, MonetaryValue freeAllowance)
    {
        if (income < 0m.Annually())
            throw new ArgumentOutOfRangeException(nameof(income), "Income value cannot be less than zero.");

        if (freeAllowance < 0m.Annually())
            throw new ArgumentOutOfRangeException(nameof(freeAllowance), "Free Allowance value cannot be less than zero.");

        var initialFreeAllowance = freeAllowance;

        var qualifyingIncome = CalculateFinalQualifyingIncome(income, initialFreeAllowance);

        return qualifyingIncome;
    }

    private MonetaryValue CalculateFinalQualifyingIncome(MonetaryValue income, MonetaryValue initialFreeAllowance)
    {
        var qualifyingIncome = income - initialFreeAllowance;

        if (qualifyingIncome < 0)
            qualifyingIncome = 0;

        return qualifyingIncome;
    }
}
