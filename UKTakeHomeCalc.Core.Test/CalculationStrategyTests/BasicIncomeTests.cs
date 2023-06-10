using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.CalculationStrategies.IncomeItem;
using UKTakeHomeCalc.Core.Calculators;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategyTests
{
    public class BasicIncomeTests
    {
        private string name = "my BasicIncome";
        private decimal value = 850.00m;
        private Frequency freq = Frequency.Weekly;

        private BasicIncome _sut;

        private Mock<ISalaryItemNode> _takeHomeSummeryMock;

        public BasicIncomeTests()
        {
            _takeHomeSummeryMock = new Mock<ISalaryItemNode>();

            _sut = new BasicIncome(name, value, freq);
        }
        [Fact]
        public void CreateSalaryItem_ShouldReturnCorrectMonetaryValueWithSameInputFrequency()
        {

            var actualResult = _sut.CreateSalaryItem(_takeHomeSummeryMock.Object);

            var expectedResult = new SalaryItem(name, new MonetaryValue(value, freq));

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
