using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests
{
    public class FrequencyTests
    {
        public static TheoryData<Frequency, int, string> ShouldConvertFrequencyToCorrectIntegerTheoryData =>
            new()
            {
                {Frequency.Weekly, 7, "Weekly Value"},
                {Frequency.Monthly, 30, "Monthly Value"},
                {Frequency.Annually, 365, "Annually Value"},
            };

        [Theory]
        [MemberData(nameof(ShouldConvertFrequencyToCorrectIntegerTheoryData))]
        public void ShouldConvertFrequencyToCorrectInteger(Frequency value, int expectedResult, string testDataName)
        {
            var actualResult = (int)value;

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
