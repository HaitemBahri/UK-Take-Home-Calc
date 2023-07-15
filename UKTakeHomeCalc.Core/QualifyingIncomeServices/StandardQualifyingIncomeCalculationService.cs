using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.FreeAllowances;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingIncomeServices;

public class StandardQualifyingIncomeCalculationService : IQualifyingIncomeCalculationService
{
    private readonly IFreeAllowance _freeAllowance;
    public StandardQualifyingIncomeCalculationService(IFreeAllowance freeAllowance)
    {
        _freeAllowance = freeAllowance;
    }
    public MonetaryValue CalculateQualifyingIncome(MonetaryValue income)
    {
        if (income < 0m.Annually())
            throw new ArgumentOutOfRangeException(nameof(income), "Income value cannot be less than zero.");

        var freeAllowanceValue = _freeAllowance.GetFreeAllowance();

        var qualifyingIncome = CalculateQualifyingIncome(income, freeAllowanceValue);

        return qualifyingIncome;
    }

    private static MonetaryValue CalculateQualifyingIncome(MonetaryValue income, MonetaryValue freeAllowanceValue)
    {
        var qualifyingSalary = income - freeAllowanceValue;

        if (qualifyingSalary < 0)
            qualifyingSalary = 0;

        return qualifyingSalary;
    }
}
