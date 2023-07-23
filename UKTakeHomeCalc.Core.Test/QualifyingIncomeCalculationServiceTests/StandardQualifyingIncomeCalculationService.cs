using Moq;
using UKTakeHomeCalc.Core.FreeAllowances;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.QualifyingIncomeCalculationServiceTests;

public class StandardQualifyingIncomeCalculationServiceTests
{
    private StandardQualifyingIncomeCalculationService _sut;

    public StandardQualifyingIncomeCalculationServiceTests()
    {
        _sut = new StandardQualifyingIncomeCalculationService();
    }

    public static TheoryData<MonetaryValue, MonetaryValue, MonetaryValue, string> AboveOrZeroIncomeTheoryData =>
            new()
            {
                { 0m.Annually(), 15000m.Annually(), 0m.Annually(), "(Zero = Income)" },
                { 159m.Weekly(), 182m.Weekly(), 0m.Weekly(), "(Zero < Income < Free Allowance)" },
                { 182m.Weekly(), 182m.Weekly(), 0m.Weekly(), "(Income = Free Allowance)" },
                { 1900m.Monthly(), 1028m.Monthly(), 872m.Monthly(), "(Free Allowance < Income)"}
            };

    [Theory]
    [MemberData(nameof(AboveOrZeroIncomeTheoryData))]
    public void CalculateFreeAllowance_ShouldReturnCorrectValue_WhenIncomeAboveOrZero(MonetaryValue income,
        MonetaryValue freeAllowance, MonetaryValue expectedResult, string testDataName)
    {
        var actualResult = _sut.CalculateQualifyingIncome(income, freeAllowance);

        Assert.Equal(expectedResult, actualResult);
    }

    public static TheoryData<MonetaryValue> BelowZeroTheoryData =>
            new()
            {
                { -10m.Weekly() },
                { -145000m.Annually() }
            };

    [Theory]
    [MemberData(nameof(BelowZeroTheoryData))]
    public void CalculateFreeAllowance_ShouldThrowArgumentOutOfRangeException_WhenIncomeBelowZero(MonetaryValue income)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = _sut.CalculateQualifyingIncome(income, 0m));
    }
}
