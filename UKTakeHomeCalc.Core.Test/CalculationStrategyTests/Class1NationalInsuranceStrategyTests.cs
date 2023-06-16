using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies.NationalInsuranceStrategy;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.QualifyingSalaryServices.QualifyingSalaryCalculationService;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategyTests
{
    public class Class1NationalInsuranceStrategyTests
    {
        private Class1NationalInsuranceStrategy _sut;
        private Mock<ISalaryItemNode> _takeHomeSummeryMock;
        private Mock<IQualifyingSalaryCalculationService> _qualifyingSalaryCalculationServiceMock;
        private Mock<IFreeAllowance> _freeAllowanceMock;

        public static IEnumerable<object[]> BelowThresholdSalary
        {
            get
            {
                var salaryTestSet1 = 120m.Weekly();
                var expectedResultTestSet1 = new SalaryItemNode("National Insurance");

                var testSet1 = new object[] { salaryTestSet1, expectedResultTestSet1 };

                return new List<object[]>()
                {
                    testSet1
                };
            }
        }

        public Class1NationalInsuranceStrategyTests()
        {
            _qualifyingSalaryCalculationServiceMock = new Mock<IQualifyingSalaryCalculationService>();
            _takeHomeSummeryMock = new Mock<ISalaryItemNode>();
            _freeAllowanceMock = new Mock<IFreeAllowance>();    
            _sut = new Class1NationalInsuranceStrategy("National Insurance", 
                _qualifyingSalaryCalculationServiceMock.Object, 
                new StandardNationalInsuranceFreeAllowance());
        }

        [Theory]
        [MemberData(nameof(BelowThresholdSalary))]
        public void CreateNationalInsuranceSalaryItem_ShouldReturnCorrectValue_WhenGivenSalary(
            MonetaryValue salary,
            ISalaryItemNode expectedResult)
        {
            _takeHomeSummeryMock.Setup(x => x.GetTotal()).Returns(salary);
            _qualifyingSalaryCalculationServiceMock.Setup(x => x.CalculateQualifyingSalary(It.IsAny<MonetaryValue>(), It.IsAny<IFreeAllowance>()))
                .Returns(0m.Annually());

            var actualResult = _sut.CreateSalaryItem(_takeHomeSummeryMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
