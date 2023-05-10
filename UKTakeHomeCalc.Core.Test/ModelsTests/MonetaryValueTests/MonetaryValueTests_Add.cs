using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.MonetaryValueTests
{
    public class MonetaryValueTests_Add
    {
        [Fact]
        public void ShouldAddMonetaryValueWithSameFrequency_AndReturnValueWithOriginalFrequency()
        {
            Frequency frequency = Frequency.WEEKLY;
            var sut = new MonetaryValue(22.88m, frequency);
            var valueToBeAdded = new MonetaryValue(399.1m, frequency);
            var expectedResult = new MonetaryValue(421.98m, frequency);

            var actualResult = sut + valueToBeAdded;

            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedResult.ValueFrequency, actualResult.ValueFrequency);
        }

        [Fact]
        public void ShouldAddMonetaryValueWithDifferentFrequency_AndReturnValueWithOriginalFrequency()
        {
            var sut = new MonetaryValue(25822.88m, Frequency.ANNUALLY);
            var valueToBeAdded = new MonetaryValue(399.1m, Frequency.WEEKLY);
            var expectedResult = new MonetaryValue(46633.09m, Frequency.ANNUALLY);

            var actualResult = sut + valueToBeAdded;

            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedResult.ValueFrequency, actualResult.ValueFrequency);
        }
    }
}
