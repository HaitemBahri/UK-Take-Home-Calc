using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategy;
using UKTakeHomeCalc.Core.FreeAllowance;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingIncomeServices;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategyTests
{
    public class EnglandTaxStrategyTests
    {
        private EnglandTaxStrategy _sut;
        private Mock<ITakeHomeSummaryComposite> _takeHomeSummerMock;

        public static IEnumerable<object[]> ZeroTaxableSalary
        {
            get
            {
                var salary = 0m.Annually();
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (England)");

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
                var salary = 15200m.Annually();
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (England)");

                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %20", -3040m.Annually()));

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
                var salary = 64500m.Annually();
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (Scotland)");

                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %20", -7540m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %40", -10720m.Annually()));

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }
        public static IEnumerable<object[]> BetweenIncomeLimitAndAdditionalRateSalary
        {
            get
            {
                var salary = 114500m.Annually();
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (Scotland)");

                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %20", -7540m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %40", -30720m.Annually()));

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }
        public static IEnumerable<object[]> AdditionalRateSalary
        {
            get
            {
                var salary = 251000m.Annually();
                var salaryItemNode = new TakeHomeSummaryComposite("Tax (Scotland)");

                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %20", -7540m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %40", -34976m.Annually()));
                salaryItemNode.AddValue(new TakeHomeSummaryItem("Tax @ %45", -56637m.Annually()));

                var testDataSet1 = new object[] { salary, salaryItemNode };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }

        public EnglandTaxStrategyTests()
        {
            _sut = new EnglandTaxStrategy("Tax (England)", 
                new IncomeLimitTaxQualifyingIncomeCalculationService(new StandardTaxFreeAllowance()),
                new TieredValueCalculator());
            _takeHomeSummerMock = new Mock<ITakeHomeSummaryComposite>();
        }

        [Theory(Skip = "will be changed to integration test")]
        [MemberData(nameof(ZeroTaxableSalary))]
        [MemberData(nameof(BasicRateSalary))]
        [MemberData(nameof(HigherRateSalary))]
        [MemberData(nameof(BetweenIncomeLimitAndAdditionalRateSalary))]
        [MemberData(nameof(AdditionalRateSalary))]
        public void CreateSalaryItem_ShouldReturnCorrectTax(MonetaryValue salary, ITakeHomeSummaryComposite expectedResult)
        {
            //_taxableSalaryCalculationServiceMock.Setup(x => x.CalculateQualifyingIncome(It.IsAny<MonetaryValue>())).Returns(salary);
            _takeHomeSummerMock.Setup(x => x.GetTotal()).Returns(It.IsAny<MonetaryValue>());

            var actualResult = _sut.CreateSalaryItem(_takeHomeSummerMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
