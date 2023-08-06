using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingIncomeServices;

public class IncomeLimitTaxQualifyingIncomeCalculationService : IQualifyingIncomeCalculationService
{
    private readonly MonetaryValue INCOME_LIMIT = 100000m.Annually();

    public MonetaryValue CalculateQualifyingIncome(MonetaryValue income, MonetaryValue freeAllowance)
    {
        if (income < 0m.Annually())
            throw new ArgumentOutOfRangeException(nameof(income), "Income value cannot be less than zero.");

        if (freeAllowance < 0m.Annually())
            throw new ArgumentOutOfRangeException(nameof(freeAllowance), "Free Allowance value cannot be less than zero.");

        var initialFreeAllowance = freeAllowance;

        var freeAllowanceReduction = CalculateFreeAllowanceReduction(income);

        var updatedFreeAllowance = CalculateUpdatedFreeAllowance(initialFreeAllowance, freeAllowanceReduction);
        
        var qualifyingIncome = CalculateFinalQualifyingIncome(income, updatedFreeAllowance);

        return qualifyingIncome;
    }

    private MonetaryValue CalculateFinalQualifyingIncome(MonetaryValue income, MonetaryValue updatedFreeAllowance)
    {
        var qualifyingIncome = income - updatedFreeAllowance;

        if (qualifyingIncome < 0)
            qualifyingIncome = 0;

        return qualifyingIncome;
    }

    private MonetaryValue CalculateUpdatedFreeAllowance(MonetaryValue initialFreeAllowance, MonetaryValue freeAllowanceReduction)
    {
        var updatedFreeAllowance = initialFreeAllowance - freeAllowanceReduction;

        if (updatedFreeAllowance < 0)
            updatedFreeAllowance = 0;

        return updatedFreeAllowance;
    }

    private MonetaryValue CalculateFreeAllowanceReduction(MonetaryValue income)
    {
        if (income > INCOME_LIMIT)
        {
            return (income - INCOME_LIMIT) / 2;
        }
        else
        {
            return 0m.Annually();
        }
    }
}
