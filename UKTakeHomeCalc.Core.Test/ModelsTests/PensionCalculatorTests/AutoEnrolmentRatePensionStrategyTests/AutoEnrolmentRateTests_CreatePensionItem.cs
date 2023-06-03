using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem;
using UKTakeHomeCalc.Core.Services.Calculator.PensionCalculator.PensionStrategy;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.PensionCalculatorTests.AutoEnrolmentRatePensionStrategyTests
{
    public class AutoEnrolmentRateTests_CreatePensionItem
    {
        public AutoEnrolmentRateTests_CreatePensionItem()
        {

        }
        [Fact]
        public void ShouldReturnZeroMonetaryValue_WhenSalaryLessThanLowerThreshold()
        {
            var name = "my hourlyRateIncome";
            var percentage = 0.05f;
            var salary = 1650.00m;
            var freq = Frequency.Annually;
            var expected = 0;

            var salaryMock = new Mock<ISalaryItemNode>();
            salaryMock.Setup(x => x.GetTotal()).Returns(new MonetaryValue(salary, freq));

            var sut = new AutoEnrolmentRatePensionStrategy(name, percentage);

            var actualResult = sut.CreatePensionSalaryItem(salaryMock.Object);
            var expectedResult = new SalaryItem(name, new MonetaryValue(expected, freq));

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShouldReturnCorrectMonetaryValue_WhenSalaryBetweenThresholds()
        {
            var name = "my hourlyRateIncome";
            var percentage = 0.05f;
            var salary = 45750.00m;
            var freq = Frequency.Annually;
            var expected = (salary - 6240m) * (decimal)percentage * (-1);

            var salaryMock = new Mock<ISalaryItemNode>();
            salaryMock.Setup(x => x.GetTotal()).Returns(new MonetaryValue(salary, freq));

            var sut = new AutoEnrolmentRatePensionStrategy(name, percentage);

            var actualResult = sut.CreatePensionSalaryItem(salaryMock.Object);
            var expectedResult = new SalaryItem(name, new MonetaryValue(expected, freq));

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShouldReturnUpperThresholdMonetaryValue_WhenSalaryGreaterThanUpperThreshold()
        {
            var name = "my hourlyRateIncome";
            var percentage = 0.05f;
            var salary = 75050.00m;
            var freq = Frequency.Annually;
            var expected = (50270m - 6240m) * (decimal)percentage * (-1);

            var salaryMock = new Mock<ISalaryItemNode>();
            salaryMock.Setup(x => x.GetTotal()).Returns(new MonetaryValue(salary, freq));

            var sut = new AutoEnrolmentRatePensionStrategy(name, percentage);

            var actualResult = sut.CreatePensionSalaryItem(salaryMock.Object);
            var expectedResult = new SalaryItem(name, new MonetaryValue(expected, freq));

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
