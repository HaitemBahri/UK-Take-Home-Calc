using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategy;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategyTests
{
    public class ScotlandTaxStrategyTests
    {
        private ScotlandTaxStrategy _sut;
        private Mock<IQualifyingIncomeCalculationService> taxableSalaryCalculationServiceMock;
        private Mock<ITakeHomeSummaryComposite> _takeHomeSummerMock;

        public static IEnumerable<object[]> ZeroTaxableSalary
        {
            get
            {
                var salary = 0m.Annually();
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (Scotland)");

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
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (Scotland)");

                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %19", -285m.Annually()));

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
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (Scotland)");

                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %19", -410.78m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %20", -1467.6m.Annually()));

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
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (Scotland)");

                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %19", -410.78m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %20", -2191.2m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %21", -2495.22m.Annually()));

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
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (Scotland)");

                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %19", -410.78m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %20", -2191.2m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %21", -3774.54m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %42", -10041.36m.Annually()));

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
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (Scotland)");

                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %19", -410.78m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %20", -2191.2m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %21", -3774.54m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %42", -39500.16m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %47", -9334.46m.Annually()));

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }

        public ScotlandTaxStrategyTests()
        {
            taxableSalaryCalculationServiceMock = new Mock<IQualifyingIncomeCalculationService>();
            _sut = new ScotlandTaxStrategy("Tax (Scotland)", taxableSalaryCalculationServiceMock.Object);
            _takeHomeSummerMock = new Mock<ITakeHomeSummaryComposite>();
        }

        [Theory]
        [MemberData(nameof(ZeroTaxableSalary))]
        [MemberData(nameof(StarterRateSalary))]
        [MemberData(nameof(BasicRateSalary))]
        [MemberData(nameof(IntermediateRateSalary))]
        [MemberData(nameof(HigherRateSalary))]
        [MemberData(nameof(TopRateSalary))]
        public void CreateSalaryItem_ShouldReturnCorrectTax(MonetaryValue salary, ITakeHomeSummaryComposite expectedResult)
        {
            taxableSalaryCalculationServiceMock.Setup(x => x.CalculateQualifyingIncome(It.IsAny<MonetaryValue>())).Returns(salary);
            _takeHomeSummerMock.Setup(x => x.GetTotal()).Returns(It.IsAny<MonetaryValue>());

            var actualResult = _sut.CreateSalaryItem(_takeHomeSummerMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }


    }
}
