using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.SalaryItemTests
{
    public  class SalaryItemTests_GetTotal
    {
        [Theory]
        [InlineData(39902.44, Frequency.Annually)]
        [InlineData(-58.18, Frequency.Weekly)]
        [InlineData(0, Frequency.Monthly)]
        public void ShouldReturnItsOwnValue(decimal value, Frequency valueFrequency)
        {
            var expectedValue = new MonetaryValue(value, valueFrequency);
            var sut = new SalaryItem("Random name", expectedValue);

            var actualResult = sut.GetTotal();

            Assert.Equal(expectedValue, actualResult);

        }
    }
}
