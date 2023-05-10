using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.MonetaryValueTests
{
    public class MonetaryValueTests_ValueComparison
    {
        [Fact]
        public void ShouldNotBeEqualToNull()
        {
            var sut = new MonetaryValue(5003.922m, Frequency.WEEKLY);
            MonetaryValue nullMonetaryValue = null!;

            Assert.NotEqual(sut, nullMonetaryValue);
        }

        [Theory]
        [InlineData(48.888, 48.89)]
        [InlineData(129.1145, 129.11)]
        [InlineData(3.145, 3.14)]
        [InlineData(3.135, 3.14)]
        public void ValueShouldBeEqualToTwoDecimalPlaces(decimal value, decimal expectedValue)
        {
            var sut = new MonetaryValue(value, Frequency.WEEKLY);
            Assert.Equal(sut.Value, expectedValue, 2);
        }

        [Fact]
        public void ShouldTwoMonetaryValuesBeEqual()
        {
            var sut = new MonetaryValue(97.99m, Frequency.WEEKLY);
            var sut2 = new MonetaryValue(97.99m, Frequency.WEEKLY);

            Assert.Equal(sut, sut2);
        }

        [Fact]
        public void ShouldTwoValuesWithSameValueAndDifferentFrequenciesBeNotEqual()
        {
            var sut = new MonetaryValue(97.99m, Frequency.MONTHLY);
            var sut2 = new MonetaryValue(97.99m, Frequency.ANNUALLY);

            Assert.NotEqual(sut, sut2);
        }

        [Theory]
        [InlineData(97.99, Frequency.MONTHLY, 1192.21, Frequency.ANNUALLY)]
        [InlineData(395.07, Frequency.WEEKLY, 1693.16, Frequency.MONTHLY)]
        public void ShouldTwoValuesWithDifferentValuesAndFrequenciesBeEqual(decimal value1, Frequency frequency1, decimal value2, Frequency frequency2)
        {
            var sut = new MonetaryValue(value1, frequency1);
            var sut2 = new MonetaryValue(value2, frequency2);

            Assert.Equal(sut, sut2);
        }
    }
}
