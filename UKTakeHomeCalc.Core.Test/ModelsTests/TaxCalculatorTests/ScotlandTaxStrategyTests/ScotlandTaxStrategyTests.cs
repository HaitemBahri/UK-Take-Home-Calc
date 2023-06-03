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

namespace UKTakeHomeCalc.Core.Test.ModelsTests.TaxCalculatorTests.ScotlandTaxStrategyTests
{
    public class ScotlandTaxStrategyTests
    {
        private ScotlandTaxStrategy _sut;
        private Mock<ITaxableSalaryCalculationService> taxableSalaryCalculationServiceMock;
        private Mock<ISalaryItem> salaryMock;

        public static IEnumerable<object[]> ZeroTaxableSalary
        {
            get
            {
                var salary = 0m.Annually();
                var salaryItemNode = new SalaryItemNode("Tax (Scotland)");

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }
        public static IEnumerable<object[]> StarterRateSalary
        {
            get
            {
                var salary = 1500m.Annually();
                var salaryItemNode = new SalaryItemNode("Tax (Scotland)");

                salaryItemNode.AddValue(new SalaryItem("Tax @ %19", -285m.Annually()));

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }
        public static IEnumerable<object[]> BasicRateSalary
        {
            get
            {
                var salary = 9500m.Annually();
                var salaryItemNode = new SalaryItemNode("Tax (Scotland)");

                salaryItemNode.AddValue(new SalaryItem("Tax @ %19", -410.78m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %20", -1467.6m.Annually()));

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }
        public static IEnumerable<object[]> IntermediateRateSalary
        {
            get
            {
                var salary = 25000m.Annually();
                var salaryItemNode = new SalaryItemNode("Tax (Scotland)");

                salaryItemNode.AddValue(new SalaryItem("Tax @ %19", -410.78m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %20", -2191.2m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %21", -2495.22m.Annually()));

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }
        public static IEnumerable<object[]> HigherRateSalary
        {
            get
            {
                var salary = 55000m.Annually();
                var salaryItemNode = new SalaryItemNode("Tax (Scotland)");

                salaryItemNode.AddValue(new SalaryItem("Tax @ %19", -410.78m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %20", -2191.2m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %21", -3774.54m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %42", -10041.36m.Annually()));

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }
        public static IEnumerable<object[]> TopRateSalary
        {
            get
            {
                var salary = 145000.55m.Annually();
                var salaryItemNode = new SalaryItemNode("Tax (Scotland)");

                salaryItemNode.AddValue(new SalaryItem("Tax @ %19", -410.78m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %20", -2191.2m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %21", -3774.54m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %42", -39500.16m.Annually()));
                salaryItemNode.AddValue(new SalaryItem("Tax @ %47", -9334.46m.Annually()));

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }

        public ScotlandTaxStrategyTests()
        {
            taxableSalaryCalculationServiceMock = new Mock<ITaxableSalaryCalculationService>();
            _sut = new ScotlandTaxStrategy("Tax (Scotland)", taxableSalaryCalculationServiceMock.Object);
            salaryMock = new Mock<ISalaryItem>();
        }

        [Theory]
        [MemberData(nameof(ZeroTaxableSalary))]
        [MemberData(nameof(StarterRateSalary))]
        [MemberData(nameof(BasicRateSalary))]
        [MemberData(nameof(IntermediateRateSalary))]
        [MemberData(nameof(HigherRateSalary))]
        [MemberData(nameof(TopRateSalary))]
        public void CreateSalaryItem_ShouldReturnCorrectTax(MonetaryValue salary, ISalaryItemNode expectedResult)
        {
            taxableSalaryCalculationServiceMock.Setup(x => x.CalculateTaxableSalary(It.IsAny<MonetaryValue>())).Returns(salary);
            salaryMock.Setup(x => x.GetTotal()).Returns(It.IsAny<MonetaryValue>());

            var actualResult = _sut.CreateTaxSalaryItem(salaryMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }


    }
}
