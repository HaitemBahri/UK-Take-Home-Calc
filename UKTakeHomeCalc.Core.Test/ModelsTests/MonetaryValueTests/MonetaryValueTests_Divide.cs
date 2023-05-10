using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.MonetaryValueTests
{
    public class MonetaryValueTests_Divide
    {
        [Theory]
        [InlineData(961.22, 1.88, 511.2872)]
        [InlineData(27852.09, 18.5, 1505.518)]
        public void ShouldDivideDecimal_AndReturnValueWithOriginalFrequency(decimal sutValue, decimal DivideValue, decimal expectedValue)
        {
            var sut = new MonetaryValue(sutValue, Frequency.ANNUALLY);
            var expectedResult = new MonetaryValue(expectedValue, Frequency.ANNUALLY);

            var actualResult = sut / DivideValue;

            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedResult.ValueFrequency, actualResult.ValueFrequency);
        }

        [Fact]
        public void ShouldThrowDivideByZeroExceptionWhenDividingByZero()
        {
            var sut = new MonetaryValue(4599.39903m, Frequency.ANNUALLY);
            var DivideValue = 0;

            Assert.Throws<DivideByZeroException>(() => sut / DivideValue);
        }
    }
}
