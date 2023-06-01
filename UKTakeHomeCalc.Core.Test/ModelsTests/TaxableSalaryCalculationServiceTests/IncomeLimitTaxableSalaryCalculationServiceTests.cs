using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxableSalaryCalculationService;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxFreeAllowance;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.TaxableSalaryCalculationServiceTests
{
    public class IncomeLimitTaxableSalaryCalculationServiceTests
    {
        private IncomeLimitTaxableSalaryCalculationService sut;
        private Mock<ITaxFreeAllowance> taxFreeAllowanceMock;

        public IncomeLimitTaxableSalaryCalculationServiceTests()
        {
            taxFreeAllowanceMock = new Mock<ITaxFreeAllowance>();
            sut = new IncomeLimitTaxableSalaryCalculationService(taxFreeAllowanceMock.Object);
        }

        [Fact]
        public void CalculateTaxableSalary_ShouldReturnZero_WhenSalaryLessThanTaxFreeAllowance()
        {
            var salary = 12950m.Annually();
            var taxFreeAllowance = 15000m.Annually();
            var expectedResult = 0m.Annually();

            taxFreeAllowanceMock.Setup(x => x.GetTaxFreeAllowance()).Returns(taxFreeAllowance);

            var actualResult = sut.CalculateTaxableSalary(salary);

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void CalculateTaxableSalary_ShouldReturnCorrectValue_WhenSalaryGreatorThanTaxFreeAllowanceAndLessThanIncomeLimit()
        {
            var salary = 52950m.Annually();
            var taxFreeAllowance = 15000m.Annually();
            var expectedResult = 37950m.Annually();

            taxFreeAllowanceMock.Setup(x => x.GetTaxFreeAllowance()).Returns(taxFreeAllowance);

            var actualResult = sut.CalculateTaxableSalary(salary);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CalculateTaxableSalary_ShouldReturnCorrectValue_WhenSalaryGreatorThanIncomeLimit()
        {
            var salary = 110950m.Annually();
            var taxFreeAllowance = 15000m.Annually();
            var expectedResult = 101425m.Annually();

            taxFreeAllowanceMock.Setup(x => x.GetTaxFreeAllowance()).Returns(taxFreeAllowance);

            var actualResult = sut.CalculateTaxableSalary(salary);

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void CalculateTaxableSalary_ShouldReturnSameSalary_WhenSalaryGreatorThanIncomeLimitAndTaxFreeAllowanceReducedToZero()
        {
            var salary = 180950m.Annually();
            var taxFreeAllowance = 15000m.Annually();
            var expectedResult = 180950m.Annually();

            taxFreeAllowanceMock.Setup(x => x.GetTaxFreeAllowance()).Returns(taxFreeAllowance);

            var actualResult = sut.CalculateTaxableSalary(salary);

            Assert.Equal(expectedResult, actualResult);
        }

    }
}
