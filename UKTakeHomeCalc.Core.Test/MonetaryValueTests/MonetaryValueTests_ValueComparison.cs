using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.MonetaryValueTests
{
    public class MonetaryValueTests_ValueComparison
    {
        [Theory]
        [InlineData(48.888, 48.89)]
        [InlineData(129.1145, 129.11)]
        [InlineData(3.145, 3.14)]
        [InlineData(3.135, 3.14)]
        public void ValueShouldBeEqualToTwoDecimalPlaces(decimal value, decimal expectedValue)
        {
            var sut = new MonetaryValue(value, Frequency.Weekly);
            Assert.Equal(sut.Value, expectedValue, 2);
        }

        [Fact]
        public void ShouldTwoMonetaryValuesBeEqual()
        {
            var sut = new MonetaryValue(97.99m, Frequency.Weekly);
            var sut2 = new MonetaryValue(97.99m, Frequency.Weekly);

            Assert.Equal(sut, sut2);
        }

        [Fact]
        public void ShouldTwoValuesWithSameValueAndDifferentFrequenciesBeNotEqual()
        {
            var sut = new MonetaryValue(97.99m, Frequency.Monthly);
            var sut2 = new MonetaryValue(97.99m, Frequency.Annually);

            Assert.NotEqual(sut, sut2);
        }

        [Theory]
        [InlineData(97.99, Frequency.Monthly, 1192.21, Frequency.Annually)]
        [InlineData(395.07, Frequency.Weekly, 1693.16, Frequency.Monthly)]
        public void ShouldTwoValuesWithDifferentValuesAndFrequenciesBeEqual(decimal value1, Frequency frequency1, decimal value2, Frequency frequency2)
        {
            var sut = new MonetaryValue(value1, frequency1);
            var sut2 = new MonetaryValue(value2, frequency2);

            Assert.Equal(sut, sut2);
        }

        [Theory]
        [InlineData(97.99, Frequency.Monthly, 1192.21, Frequency.Annually)]
        [InlineData(395.07, Frequency.Weekly, 1693.16, Frequency.Monthly)]
        public void ShouldTwoValuesBeEqualUsingEqualOperator(decimal value1, Frequency frequency1, decimal value2, Frequency frequency2)
        {
            var sut = new MonetaryValue(value1, frequency1);
            var sut2 = new MonetaryValue(value2, frequency2);

            var actualResult1 = (sut == sut2);
            var actualResult2 = (sut != sut2);

            Assert.True(actualResult1);
            Assert.False(actualResult2);
        }

        [Theory]
        [InlineData(9.19, Frequency.Monthly, 1192.21, Frequency.Annually)]
        [InlineData(3555.07, Frequency.Weekly, 1693.16, Frequency.Monthly)]
        public void ShouldTwoValuesNotBeEqualUsingEqualOperator(decimal value1, Frequency frequency1, decimal value2, Frequency frequency2)
        {
            var sut = new MonetaryValue(value1, frequency1);
            var sut2 = new MonetaryValue(value2, frequency2);

            var actualResult1 = (sut != sut2);
            var actualResult2 = (sut == sut2);

            Assert.True(actualResult1);
            Assert.False(actualResult2);
        }
        [Theory]
        [InlineData(658.19, Frequency.Weekly, 1192.21, Frequency.Annually)]
        [InlineData(3555.07, Frequency.Weekly, 1693.16, Frequency.Monthly)]
        public void ShouldOneValueBeGreaterThanOtherValue(decimal value1, Frequency frequency1, decimal value2, Frequency frequency2)
        {
            var sut = new MonetaryValue(value1, frequency1);
            var sut2 = new MonetaryValue(value2, frequency2);

            Assert.True(sut > sut2, "Value 1 is not greater than Value 2");
        }
        [Theory]
        [InlineData(658.19, Frequency.Weekly, 1192.21, Frequency.Annually)]
        [InlineData(3555.07, Frequency.Weekly, 3555.07, Frequency.Weekly)]
        public void ShouldOneValueBeGreaterThanOrEqualOtherValue(decimal value1, Frequency frequency1, decimal value2, Frequency frequency2)
        {
            var sut = new MonetaryValue(value1, frequency1);
            var sut2 = new MonetaryValue(value2, frequency2);

            Assert.True(sut >= sut2, "Value 1 is not greater than or equal Value 2");
        }
        [Theory]
        [InlineData(1192.21, Frequency.Annually , 658.19, Frequency.Weekly)]
        [InlineData( 1693.16, Frequency.Monthly, 3555.07, Frequency.Weekly)]
        public void ShouldOneValueBeLessThanOtherValue(decimal value1, Frequency frequency1, decimal value2, Frequency frequency2)
        {
            var sut = new MonetaryValue(value1, frequency1);
            var sut2 = new MonetaryValue(value2, frequency2);

            Assert.True(sut < sut2, "Value 1 is not less than Value 2");
        }
        [Theory]
        [InlineData(1192.21, Frequency.Annually, 658.19, Frequency.Weekly)]
        [InlineData(3555.07, Frequency.Weekly, 3555.07, Frequency.Weekly)]
        public void ShouldOneValueBeLessThanOrEqualOtherValue(decimal value1, Frequency frequency1, decimal value2, Frequency frequency2)
        {
            var sut = new MonetaryValue(value1, frequency1);
            var sut2 = new MonetaryValue(value2, frequency2);

            Assert.True(sut <= sut2, "Value 1 is not less than or equal Value 2");
        }
    }
}
