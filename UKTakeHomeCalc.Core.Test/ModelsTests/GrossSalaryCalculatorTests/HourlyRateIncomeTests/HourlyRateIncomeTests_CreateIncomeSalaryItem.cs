using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.GrossSalaryCalculatorTests.HourlyRateIncomeTests
{
    public class HourlyRateIncomeTests_CreateIncomeSalaryItem
    {
        [Fact]
        public void ShouldReturnCorrectMonetaryValueWithSameInputFrequency()
        {
            var name = "my hourlyRateIncome";
            var hourlyRate = 15.48m;
            var hours = 37.5m;
            var freq = Frequency.Weekly;

            var sut = new HourlyRateIncome(name, hourlyRate, hours, freq);

            var actualResult = sut.CreateIncomeSalaryItem();
            var expectedResult = new SalaryItem(name, new MonetaryValue(hourlyRate * hours, freq));

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
