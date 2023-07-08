using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies.IncomeItem;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategyTests
{
    public class HourlyRateIncomeTests
    {
        private string name = "my hourlyRateIncome";
        private decimal hourlyRate = 15.48m;
        private decimal hours = 37.5m;
        private Frequency freq = Frequency.Weekly;

        private HourlyRateIncome _sut;

        private Mock<ITakeHomeSummaryComposite> _takeHomeSummeryMock;

        public HourlyRateIncomeTests()
        {
            _takeHomeSummeryMock = new Mock<ITakeHomeSummaryComposite>();

            _sut = new HourlyRateIncome(name, hourlyRate, hours, freq);
        }

        [Fact]
        public void CreateIncomeSalaryItem_ShouldReturnCorrectMonetaryValueWithSameInputFrequency()
        {
            var actualResult = _sut.CreateSalaryItem(_takeHomeSummeryMock.Object);

            var expectedResult = new TakeHomeSummaryItem(name, new MonetaryValue(hourlyRate * hours, freq));

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
