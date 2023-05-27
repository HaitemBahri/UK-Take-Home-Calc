using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.SalaryItemTests
{
    public class SalaryItemTests_ValueComparison
    {
        [Fact]
        public void ShouldTwoSalaryItemsWithSameNameAndValueBeEqual()
        {
            var sut1 = new SalaryItem("some salaryItem", new MonetaryValue(52.2m, Frequency.WEEKLY));
            var sut2 = new SalaryItem("some salaryItem", new MonetaryValue(52.2m, Frequency.WEEKLY));

            Assert.Equal(sut1, sut2);
        }

        [Fact]
        public void ShouldTwoSalaryItemsWithSameNameAndDifferentValueNotBeEqual()
        {
            var sut1 = new SalaryItem("some salaryItem", new MonetaryValue(455.7m, Frequency.MONTHLY));
            var sut2 = new SalaryItem("some salaryItem", new MonetaryValue(52.2m, Frequency.MONTHLY));

            Assert.NotEqual(sut1, sut2);
        }

        [Fact]
        public void ShouldTwoSalaryItemsWithDifferentNameAndSameValueNotBeEqual()
        {
            var sut1 = new SalaryItem("some salaryItem - One", new MonetaryValue(52.2m, Frequency.ANNUALLY));
            var sut2 = new SalaryItem("some salaryItem - Two", new MonetaryValue(52.2m, Frequency.ANNUALLY));

            Assert.NotEqual(sut1, sut2);
        }
    }
}
