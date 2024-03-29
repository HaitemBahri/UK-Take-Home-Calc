﻿using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.QualifyingIncomeCalculationServiceTests
{
    public class IncomeLimitTaxQualifyingIncomeCalculationServiceTests
    {
        private IncomeLimitTaxQualifyingIncomeCalculationService _sut;

        public IncomeLimitTaxQualifyingIncomeCalculationServiceTests()
        {
            _sut = new IncomeLimitTaxQualifyingIncomeCalculationService();
        }

        public static TheoryData<MonetaryValue, MonetaryValue, MonetaryValue, string> AboveOrZeroIncomeTheoryData =>
            new()
            {
                { 0m.Annually(), 15000m.Annually(), 0m.Annually(), "(Income = Zero) Test Data"},
                { 159m.Weekly(), 182m.Weekly(), 0m.Weekly(), "(Zero > Income > FreeAllowance) Test Data"},
                { 182m.Weekly(), 182m.Weekly(), 0m.Weekly(), "(Income = FreeAllowance) Test Data"},
                { 1900m.Monthly(), 1028m.Monthly(), 872m.Monthly(), "(FreeAllowance > Income > Income Limit) Test Data"},
                { 100000m.Annually(), 12750m.Annually(), 87250m.Annually(), "(Income = Income Limit) Test Data"},
                { 112500m.Annually(), 12750m.Annually(), 106000m.Annually(), "(Income Limit < Income) Test Data"},
                { 250000m.Annually(), 15700m.Annually(), 250000m.Annually(), "(Income Limit << Income & Free Allowance becomes Zero) Test Data"},
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
}
