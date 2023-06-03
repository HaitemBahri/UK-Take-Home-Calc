using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.SalaryItemTests
{
    public class SalaryItemTests_Value
    {
        [Fact]
        public void ShouldReturnItsNameAndValue()
        {
            var expectedName = "SalaryBreakdown Item Some Name";
            var expectedValue = new MonetaryValue(499.22m, Frequency.Monthly);
            var sut = new SalaryItem(expectedName, expectedValue);

            var actualName = sut.Name;
            var actualValue = sut.Value;

            Assert.Equal(expectedName, actualName);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
