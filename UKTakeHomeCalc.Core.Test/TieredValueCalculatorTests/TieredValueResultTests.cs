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
    public class TieredValueResultTests
    {
        private TieredValueResult _sut;
        public TieredValueResultTests()
        {
            var rule = new TieredValueRule(1000m.Monthly(), 1500m.Monthly(), 0.12m);
            _sut = new TieredValueResult(rule, 56.332m.Annually());
        }

        [Fact]
        public void ShouldReturnCorrectString()
        {
            var expectedResult = $"Rule = [From 1,000.00/Monthly - To 1,500.00/Monthly] %12.00, Result = 56.33/Annually";

            var actualResult = _sut.ToString();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
