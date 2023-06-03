using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.MonetaryValueTests
{
    public class MonetaryValueTests_Multiply
    {
        [Theory]
        [InlineData(23.11, 5.5, 127.11)]
        [InlineData(19855.333, 1.228, 24382.35)]
        public void ShouldMultiplyDecimal_AndReturnValueWithOriginalFrequency(decimal sutValue, decimal multiplyValue, decimal expectedValue)
        {
            var sut = new MonetaryValue(sutValue, Frequency.Monthly);
            var expectedResult = new MonetaryValue(expectedValue, Frequency.Monthly);

            var actualResult = sut * multiplyValue;

            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedResult.ValueFrequency, actualResult.ValueFrequency);
        }
    }
}
