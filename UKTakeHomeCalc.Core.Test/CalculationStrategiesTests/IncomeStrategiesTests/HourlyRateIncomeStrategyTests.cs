using UKTakeHomeCalc.Core.CalculationStrategies.IncomeStrategies;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategiesTests.IncomeStrategiesTests
{
    public class HourlyRateIncomeStrategyTests
    {
        private HourlyRateIncomeStrategy _sut;
        private readonly static string _sutName = "Hourly Rate Income";

        public static TheoryData<decimal, decimal, Frequency, TakeHomeSummaryItem, string> ShouldReturnCorrectTakeHomeSummaryItemTheoryData =>
            new()
            {
                {11.23m, 41.5m, Frequency.Weekly, new TakeHomeSummaryItem(_sutName, 466.045m.Weekly()), "Weekly Income" },
                {15.30m, 120m, Frequency.Monthly, new TakeHomeSummaryItem(_sutName, 1836m.Monthly()), "Monthly Income" },
            };

        [Theory]
        [MemberData(nameof(ShouldReturnCorrectTakeHomeSummaryItemTheoryData))]
        public void ShouldReturnCorrectTakeHomeSummaryItem(decimal rate, decimal hours, Frequency frequency, TakeHomeSummaryItem expectedResult, string testDataName)
        {
            _sut = new HourlyRateIncomeStrategy(_sutName, rate, hours, frequency);

            var actualResult = _sut.CreateTakeHomeSummaryItem(null!);

            Assert.Equal(expectedResult, actualResult);
        }

        public static TheoryData<decimal, decimal, Frequency, TakeHomeSummaryItem, string> ShouldThrowArgumentOutOfRangeException_WhenValueIsNegativeTheoryData =>
            new()
            {
                {-11.23m, 41.5m, Frequency.Weekly, new TakeHomeSummaryItem(_sutName, 466.045m.Weekly()), " Negative Hourly Rate" },
                {16.30m, -120m, Frequency.Monthly, new TakeHomeSummaryItem(_sutName, 1836m.Monthly()), "Negative Hours" },
                {-15.30m, -110m, Frequency.Annually, new TakeHomeSummaryItem(_sutName, 1836m.Monthly()), "Negative Hourly Rate + Negative Hours" },
            };

        [Theory]
        [MemberData(nameof(ShouldThrowArgumentOutOfRangeException_WhenValueIsNegativeTheoryData))]
        public void ShouldThrowArgumentOutOfRangeException_WhenValueIsNegative(decimal rate, decimal hours, Frequency frequency, TakeHomeSummaryItem expectedResult, string testDataName)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut = new HourlyRateIncomeStrategy(_sutName, rate, hours, frequency));
        }
    }
}
