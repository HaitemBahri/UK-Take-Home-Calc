using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies.PensionStrategy;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategyTests
{
    public class AutoEnrolmentRatePensionStrategyTests
    {
        private string _name = "autoEnrolmentRatePension";
        private float _percentage = 0.05f;
        private Mock<ITakeHomeSummaryComposite> _takeHomeSummeryMock;
        private AutoEnrolmentRatePensionStrategy _sut;

        public static IEnumerable<object[]> SalaryLessThanLowerThreshold
        {
            get
            {

                var salary = 1650.00m.Annually();
                var expectedResult = new TakeHomeSummaryItem("autoEnrolmentRatePension", 0m.Annually());

                var testDataSet1 = new object[] { salary, expectedResult };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }

        public static IEnumerable<object[]> BetweenThresholds
        {
            get
            {

                var salary = 45750.00m.Annually();
                var expectedResult = new TakeHomeSummaryItem("autoEnrolmentRatePension", -1975.5m.Annually());

                var testDataSet1 = new object[] { salary, expectedResult };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }

        public static IEnumerable<object[]> SalaryGreaterThanUpperThreshold
        {
            get
            {

                var salary = 75050m.Annually();
                var expectedResult = new TakeHomeSummaryItem("autoEnrolmentRatePension", -2201.5m.Annually());

                var testDataSet1 = new object[] { salary, expectedResult };

                return new List<object[]>()
                {
                    testDataSet1
                };
            }
        }

        public AutoEnrolmentRatePensionStrategyTests()
        {

            _takeHomeSummeryMock = new Mock<ITakeHomeSummaryComposite>();

            _sut = new AutoEnrolmentRatePensionStrategy(_name, _percentage);

        }

        [Theory]
        [MemberData(nameof(SalaryLessThanLowerThreshold))]
        [MemberData(nameof(BetweenThresholds))]
        [MemberData(nameof(SalaryGreaterThanUpperThreshold))]
        public void CreatePensionItem_ShouldReturnZeroMonetaryValue_WhenSalaryLessThanLowerThreshold(
            MonetaryValue salary, ITakeHomeSummaryItem expectedResult)
        {
            _takeHomeSummeryMock.Setup(x => x.GetTotal()).Returns(salary);
            var actualResult = _sut.CreateSalaryItem(_takeHomeSummeryMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
