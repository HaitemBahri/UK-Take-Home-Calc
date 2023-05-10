using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.MonetaryValueTests
{
    public class MonetaryValueTests_Subtract
    {
        [Fact]
        public void ShouldSubtractMonetaryValueWithSameFrequency_AndReturnValueWithOriginalFrequency()
        {
            Frequency frequency = Frequency.WEEKLY;
            var sut = new MonetaryValue(9822.88999m, frequency);
            var valueToBeSubtracted = new MonetaryValue(399.450001m, frequency);
            var expectedResult = new MonetaryValue(9423.439m, frequency);

            var actualResult = sut - valueToBeSubtracted;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldSubtractMonetaryValueWithDifferentFrequency_AndReturnValueWithOriginalFrequency()
        {
            var sut = new MonetaryValue(25822.881m, Frequency.ANNUALLY);
            var valueToBeSubtracted = new MonetaryValue(399.1m, Frequency.MONTHLY);
            var expectedResult = new MonetaryValue(20967.16433m, Frequency.ANNUALLY);

            var actualResult = sut - valueToBeSubtracted;

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
