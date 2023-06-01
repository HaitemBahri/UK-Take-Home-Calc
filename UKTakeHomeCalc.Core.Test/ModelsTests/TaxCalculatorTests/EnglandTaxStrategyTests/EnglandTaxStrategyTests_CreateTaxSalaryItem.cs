using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxableSalaryCalculationService;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxFreeAllowance;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxStrategy;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.TaxCalculatorTests.EnglandTaxStrategyTests
{
    public class EnglandTaxStrategyTests_CreateTaxSalaryItem
    {
        [Theory]
        [InlineData(12570, 9175.50, 0)]
        [InlineData(5570, 4175.50, 0)]
        public void ShouldReturnZeroTax_WhenSalaryLessThanPersonalAllowance(decimal personalAllowanceAnnually,
            decimal SalaryAnnually,
            decimal expectedValue)
        {
            var salary = new MonetaryValue(SalaryAnnually, Frequency.ANNUALLY);
            var sut = new EnglandTaxStrategy("some name", 
                new IncomeLimitTaxableSalaryCalculationService(
                    new TaxFreeAllowance(personalAllowanceAnnually.Annually())));
            var salaryMock = new Mock<ISalaryItem>();
            var expectedResult = new MonetaryValue(expectedValue, Frequency.ANNUALLY);

            salaryMock.Setup(x => x.GetTotal()).Returns(salary);

            var actualResult = sut.CreateTaxSalaryItem(salaryMock.Object);
            Assert.Equal(expectedResult, actualResult.GetTotal());
        }
        [Theory]
        [InlineData(12570, 29175.50, -3321.1)]
        [InlineData(9560, 19175.50, -1923.1)]
        public void ShouldReturnCorrectTax_WhenSalaryIsAtBasicRate(decimal personalAllowanceAnnually,
            decimal SalaryAnnually,
            decimal expectedValue)
        {
            var salary = new MonetaryValue(SalaryAnnually, Frequency.ANNUALLY);
            var sut = new EnglandTaxStrategy("some name", 
                new IncomeLimitTaxableSalaryCalculationService(
                    new TaxFreeAllowance(personalAllowanceAnnually.Annually())));
            var salaryMock = new Mock<ISalaryItem>();
            var expectedResult = new MonetaryValue(expectedValue, Frequency.ANNUALLY);

            salaryMock.Setup(x => x.GetTotal()).Returns(salary);

            var actualResult = sut.CreateTaxSalaryItem(salaryMock.Object);
            Assert.Equal(expectedResult, actualResult.GetTotal());
        }
        [Theory]
        [InlineData(12570, 69100.50, -15072.2)]
        [InlineData(9560, 49175.50, -8306.2)]
        public void ShouldReturnCorrectTax_WhenSalaryIsAtHigherRateAndLessThanIncomeLimit(decimal personalAllowanceAnnually,
            decimal SalaryAnnually,
            decimal expectedValue)
        {
            var salary = new MonetaryValue(SalaryAnnually, Frequency.ANNUALLY);
            var sut = new EnglandTaxStrategy("some name",
                new IncomeLimitTaxableSalaryCalculationService(
                    new TaxFreeAllowance(personalAllowanceAnnually.Annually())));
            var salaryMock = new Mock<ISalaryItem>();
            var expectedResult = new MonetaryValue(expectedValue, Frequency.ANNUALLY);

            salaryMock.Setup(x => x.GetTotal()).Returns(salary);

            var actualResult = sut.CreateTaxSalaryItem(salaryMock.Object);
            Assert.Equal(expectedResult, actualResult.GetTotal());
        }

        [Theory]
        [InlineData(12570, 115100.50, -36492.20)]
        [InlineData(15440.00, 109175.50, -31789)]
        public void ShouldReturnCorrectTax_WhenSalaryIsAtHigherRateAndGreaterThanIncomeLimit(decimal personalAllowanceAnnually,
            decimal SalaryAnnually,
            decimal expectedValue)
        {
            var salary = new MonetaryValue(SalaryAnnually, Frequency.ANNUALLY);
            var sut = new EnglandTaxStrategy("some name",
                new IncomeLimitTaxableSalaryCalculationService(
                    new TaxFreeAllowance(personalAllowanceAnnually.Annually()))); var salaryMock = new Mock<ISalaryItem>();
            var expectedResult = new MonetaryValue(expectedValue, Frequency.ANNUALLY);

            salaryMock.Setup(x => x.GetTotal()).Returns(salary);

            var actualResult = sut.CreateTaxSalaryItem(salaryMock.Object);
            Assert.Equal(expectedResult, actualResult.GetTotal());
        }

        [Theory]
        [InlineData(12570, 169100.50, -62298.23)]
        [InlineData(15440.00, 149175.50, -53331.98)]
        public void ShouldReturnCorrectTax_WhenSalaryIsAtAdditionalRate(decimal personalAllowanceAnnually,
            decimal SalaryAnnually,
            decimal expectedValue)
        {
            var salary = new MonetaryValue(SalaryAnnually, Frequency.ANNUALLY);
            var sut = new EnglandTaxStrategy("some name",
                new IncomeLimitTaxableSalaryCalculationService(
                    new TaxFreeAllowance(personalAllowanceAnnually.Annually()))); var salaryMock = new Mock<ISalaryItem>();
            var expectedResult = new MonetaryValue(expectedValue, Frequency.ANNUALLY);

            salaryMock.Setup(x => x.GetTotal()).Returns(salary);

            var actualResult = sut.CreateTaxSalaryItem(salaryMock.Object);
            Assert.Equal(expectedResult, actualResult.GetTotal());
        }
    }
}
