using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TieredValueCalculators;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.TieredValueCalculatorTests
{
    public class TieredValueRuleTests
    {
        private TieredValueRule _sut;
        public TieredValueRuleTests()
        {
            _sut = new TieredValueRule(1000m.Monthly(), 1500m.Monthly(), 0.12m);
        }

        [Fact]
        public void ShouldReturnCorrectString()
        {
            var expectedResult = $"[From 1,000.00/Monthly - To 1,500.00/Monthly] %12.00";

            var actualResult = _sut.ToString();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
