using UKTakeHomeCalc.Core.CalculationStrategies.IncomeStrategies;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategiesTests.IncomeStrategiesTests
{
    public class BasicIncomeStrategyTests
    {
        private BasicIncomeStrategy _sut;
        private readonly static string _sutName = "Basic Income";

        public static TheoryData<decimal, Frequency, TakeHomeSummaryItem, string> ShouldReturnCorrectTakeHomeSummaryItemTheoryData =>
            new()
            {
                {425.666m, Frequency.Weekly, new TakeHomeSummaryItem(_sutName, 425.666m.Weekly()), "Weekly Income" },
                {425.666m, Frequency.Monthly, new TakeHomeSummaryItem(_sutName, 425.666m.Monthly()), "Monthly Income" },
                {425.666m, Frequency.Annually, new TakeHomeSummaryItem(_sutName, 425.666m.Annually()), "Annually Income" }
            };

        [Theory]
        [MemberData(nameof(ShouldReturnCorrectTakeHomeSummaryItemTheoryData))]
        public void ShouldReturnCorrectTakeHomeSummaryItem(decimal value, Frequency frequency, TakeHomeSummaryItem expectedResult, string testDataName)
        {
            _sut = new BasicIncomeStrategy(_sutName, value, frequency);

            var actualResult = _sut.CreateTakeHomeSummaryItem(null!);

            Assert.Equal(expectedResult, actualResult);
        }

        public static TheoryData<decimal, Frequency, TakeHomeSummaryItem, string> ShouldThrowArgumentOutOfRangeException_WhenValueIsNegativeTheoryData =>
            new()
            {
                {-425.666m, Frequency.Weekly, new TakeHomeSummaryItem(_sutName, 425.666m.Weekly()), "Negative Weekly Income" },
                {-425.666m, Frequency.Monthly, new TakeHomeSummaryItem(_sutName, 425.666m.Monthly()), "Negative Monthly Income" },
                {-425.666m, Frequency.Annually, new TakeHomeSummaryItem(_sutName, 425.666m.Annually()), "Negative Annually Income" }
            };

        [Theory]
        [MemberData(nameof(ShouldThrowArgumentOutOfRangeException_WhenValueIsNegativeTheoryData))]
        public void ShouldThrowArgumentOutOfRangeException_WhenValueIsNegative(decimal value, Frequency frequency, TakeHomeSummaryItem expectedResult, string testDataName)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut = new BasicIncomeStrategy(_sutName, value, frequency));
        }
    }
}
