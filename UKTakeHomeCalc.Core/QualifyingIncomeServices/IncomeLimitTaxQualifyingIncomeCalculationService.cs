using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.QualifyingIncomeServices;

public class IncomeLimitTaxQualifyingIncomeCalculationService : IQualifyingIncomeCalculationService
{
    private readonly MonetaryValue INCOME_LIMIT = 100000m.Annually();

    public MonetaryValue CalculateQualifyingIncome(MonetaryValue income, MonetaryValue freeAllowance)
    {
        if (income < 0m.Annually())
            throw new ArgumentOutOfRangeException(nameof(income), "Income value cannot be less than zero.");

        var initialTaxFreeAllowance = freeAllowance;

        var taxFreeAllowanceReduction = CalculateTaxFreeAllowanceReduction(income);

        var updatedTaxFreeAllowance = CalculateUpdatedTaxFreeAllowance(initialTaxFreeAllowance, taxFreeAllowanceReduction);
        
        var qualifyingIncome = CalculateFinalQualifyingIncome(income, updatedTaxFreeAllowance);

        return qualifyingIncome;
    }

    private MonetaryValue CalculateFinalQualifyingIncome(MonetaryValue income, MonetaryValue updatedTaxFreeAllowance)
    {
        var taxableSalary = income - updatedTaxFreeAllowance;
        if (taxableSalary < 0)
            taxableSalary = 0;
        return taxableSalary;
    }

    private MonetaryValue CalculateUpdatedTaxFreeAllowance(MonetaryValue initialTaxFreeAllowance, MonetaryValue taxFreeAllowanceReduction)
    {
        var updatedTaxFreeAllowance = initialTaxFreeAllowance - taxFreeAllowanceReduction;

        if (updatedTaxFreeAllowance < 0)
            updatedTaxFreeAllowance = 0;

        return updatedTaxFreeAllowance;
    }

    private MonetaryValue CalculateTaxFreeAllowanceReduction(MonetaryValue income)
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
