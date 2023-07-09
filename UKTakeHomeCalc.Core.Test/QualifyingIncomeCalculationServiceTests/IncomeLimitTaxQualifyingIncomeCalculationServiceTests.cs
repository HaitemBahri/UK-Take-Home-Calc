using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.FreeAllowances;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.QualifyingIncomeCalculationServiceTests
{
    public class IncomeLimitTaxQualifyingIncomeCalculationServiceTests
    {
        private IncomeLimitTaxQualifyingIncomeCalculationService _sut;
        private Mock<IFreeAllowance> freeAllowanceMock;

        public IncomeLimitTaxQualifyingIncomeCalculationServiceTests()
        {
            freeAllowanceMock = new Mock<IFreeAllowance>();
            _sut = new IncomeLimitTaxQualifyingIncomeCalculationService(freeAllowanceMock.Object);
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
            freeAllowanceMock.Setup(x => x.GetFreeAllowance()).Returns(freeAllowance);

            var actualResult = _sut.CalculateQualifyingIncome(income);

            Assert.Equal(expectedResult, actualResult);
        }

        public static TheoryData<MonetaryValue> BelowZeroTheoryData =>
            new()
            {
                { -10m.Weekly() },
                { -145000m.Annually() },
            };

        [Theory]
        [MemberData(nameof(BelowZeroTheoryData))]
        public void CalculateFreeAllowance_ShouldThrowArgumentOutOfRangeException_WhenIncomeBelowZero(MonetaryValue income)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = _sut.CalculateQualifyingIncome(income));
        }
    }
}
