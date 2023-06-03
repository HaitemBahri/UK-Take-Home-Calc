using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.GrossSalaryCalculatorTests.BasicIncomeTests
{
    public class BasicIncomeTests_CreateIncomeSalaryItem
    {
        [Fact]
        public void ShouldReturnCorrectMonetaryValueWithSameInputFrequency()
        {
            var name = "my BasicIncome";
            var value = 850.00m;
            var freq = Frequency.Weekly;

            var sut = new BasicIncome(name, value, freq);

            var actualResult = sut.CreateIncomeSalaryItem();
            var expectedResult = new SalaryItem(name, new MonetaryValue(value, freq));

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
