using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TieredValueCalculators;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.TieredValueCalculatorTests
{
    public class TieredValueRulesBuilderTests
    {
        private TieredValueRulesBuilder _sut;

        public TieredValueRulesBuilderTests()
        {
            _sut = new TieredValueRulesBuilder();
        }

        [Fact]
        public void ShouldBuild()
        {
            var expectedResult = new List<TieredValueRule>();
            expectedResult.Add(new TieredValueRule(100m.Annually(), 900m.Annually(), 0.12m));
            expectedResult.Add(new TieredValueRule(900m.Annually(), 1900m.Annually(), 0.22m));
            expectedResult.Add(new TieredValueRule(1900m.Annually(), 2900m.Annually(), 0.32m));
            expectedResult.Add(new TieredValueRule(2900m.Annually(), 5000m.Annually(), 0.60m));

            _sut.Add(100m.Annually(), 900m.Annually(), 0.12m)
                .Add(900m.Annually(), 1900m.Annually(), 0.22m)
                .Add(1900m.Annually(), 2900m.Annually(), 0.32m)
                .Add(2900m.Annually(), 5000m.Annually(), 0.60m);

            var actualResult = _sut.Build();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
