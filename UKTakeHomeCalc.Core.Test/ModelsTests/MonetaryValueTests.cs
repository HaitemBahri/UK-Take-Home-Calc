using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests
{
    public class MonetaryValueTests 
    {
        [Theory]
        [InlineData(48.888, 48.89)]
        [InlineData(129.1145, 129.11)]
        public void ValueShouldBeEqualToTwoDecimalPlaces(decimal value, decimal expectedValue)
        {
            var sut = new MonetaryValue(value, Frequency.WEEKLY);
            Assert.Equal(sut.Value, expectedValue);
        }
        [Fact]
        public void ShouldTwoMonetaryValuesBeEqual()
        {
            var sut = new MonetaryValue(97.99m, Frequency.WEEKLY);
            var sut2 = new MonetaryValue(97.99m, Frequency.WEEKLY);

            Assert.Equal(sut, sut2);
        }

        [Fact]
        public void ShouldTwoValuesWithDifferentFrequenciesBeNotEqual()
        {
            var sut = new MonetaryValue(97.99m, Frequency.MONTHLY);
            var sut2 = new MonetaryValue(97.99m, Frequency.ANNUALLY);

            Assert.NotEqual(sut, sut2);
        }
        [Fact]
        public void ShouldTwoValuesWithDifferentValuesAndFrequenciesBeEqual()
        {
            var sut = new MonetaryValue(97.99m, Frequency.MONTHLY);
            var sut2 = new MonetaryValue(1192.21m, Frequency.ANNUALLY);

            Assert.Equal(sut, sut2);
        }
        [Fact]
        public void ShouldAddMonetaryValueWithSameFrequency_AndReturnValueWithOriginalFrequency()
        {
            Frequency frequency = Frequency.WEEKLY;
            var sut = new MonetaryValue(22.88m, frequency);
            var valueToBeAdded = new MonetaryValue(399.1m, frequency);
            var expectedResult = new MonetaryValue(421.98m, frequency);

            var actualResult = sut + valueToBeAdded;

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShouldAddMonetaryValueWithDifferentFrequency_AndReturnValueWithOriginalFrequency()
        {
            var sut = new MonetaryValue(25822.88m, Frequency.ANNUALLY);
            var valueToBeAdded = new MonetaryValue(399.1m, Frequency.WEEKLY);
            var expectedResult = new MonetaryValue(46633.09m, Frequency.ANNUALLY);

            var actualResult = sut + valueToBeAdded;

            Assert.Equal(expectedResult, actualResult);
        }
        //[Theory]
        //[InlineData(23.11, 5.5, 127.11)]
        //[InlineData(19855.333, 1.228, 12102.35)]
        //public void ShouldMultiplyDecimal_AndReturnValueWithOriginalFrequency(decimal sutValue, decimal multiplyValue, decimal expectedValue)
        //{
        //    var sut = new MonetaryValue(sutValue, Frequency.MONTHLY);
        //    var expected = new MonetaryValue(expectedValue, Frequency.MONTHLY);

        //    sut.Multiply(multiplyValue);

        //    Assert.Equal(expected, sut);
        //}
    }
}
