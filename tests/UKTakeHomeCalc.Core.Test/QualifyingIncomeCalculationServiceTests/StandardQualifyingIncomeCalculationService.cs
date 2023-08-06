using Moq;
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

    [Fact]
    public void ShouldThrowArgumentOutOfRangeException_WhenIncomeIsNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = _sut.CalculateQualifyingIncome(-10m.Weekly(), 0m));
    }

    [Fact]
    public void ShouldThrowArgumentOutOfRangeException_WhenFreeAllowanceIsNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = _sut.CalculateQualifyingIncome(1000m.Monthly(), -1500m.Annually()));
    }
}
