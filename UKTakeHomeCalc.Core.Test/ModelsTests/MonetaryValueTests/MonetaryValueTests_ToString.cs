using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.MonetaryValueTests
{
    public class MonetaryValueTests_ToString
    {
        [Fact]
        public void ShouldReturnCorrectString()
        {
            var sut = new MonetaryValue(592.66m, Frequency.WEEKLY);

            var expectedResult = "592.66/WEEKLY";

            var actualResult = sut.ToString();

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShouldReturnCorrectStringWithTwoDecimalPlaces()
        {
            var sut = new MonetaryValue(1980.95877m, Frequency.MONTHLY);

            var expectedResult = "1980.96/MONTHLY";

            var actualResult = sut.ToString();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
