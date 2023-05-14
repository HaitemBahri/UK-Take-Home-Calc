using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.SalaryItemTests
{
    public class SalaryItemTests_FindValue
    {
        [Fact]
        public void ShouldReturnItself_WhenItsNamePassed()
        {
            var monetaryValue = new MonetaryValue(4990.22m, Frequency.MONTHLY);
            var salaryItemName = "Some Salary Item";
            var sut = new SalaryItem(salaryItemName, monetaryValue);

            var actualResult = sut.FindValue(salaryItemName);

            Assert.Equal(sut, actualResult);
        }

        [Fact]
        public void ShouldReturnNull_WhenNotItsNamePassed()
        {
            var monetaryValue = new MonetaryValue(4990.22m, Frequency.MONTHLY);
            var salaryItemName = "Some Salary Item";
            var sut = new SalaryItem(salaryItemName, monetaryValue);
            var randomName = "Random Salary Item Name";

            var actualResult = sut.FindValue(randomName);

            Assert.Null(actualResult);
        }
    }
}
