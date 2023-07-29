using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TieredValueCalculators;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.TieredValueCalculatorTests
{
    public class TieredValueResultsBuilderTests
    {
        private readonly TieredValueResultsBuilder _sut = new ();

        [Fact]
        public void ShouldBuild()
        {
            var expectedResult = new List<TieredValueResult>();
            expectedResult.Add(new TieredValueResult(new TieredValueRule(100m.Annually(), 900m.Annually(), 0.12m), 123.23m.Annually()));
            expectedResult.Add(new TieredValueResult(new TieredValueRule(900m.Annually(), 1900m.Annually(), 0.22m), 19.6m.Annually()));
            expectedResult.Add(new TieredValueResult(new TieredValueRule(1900m.Annually(), 2900m.Annually(), 0.32m), 5m.Annually()));
            expectedResult.Add(new TieredValueResult(new TieredValueRule(2900m.Annually(), 5000m.Annually(), 0.60m), 0m.Annually()));

            _sut.Add(new TieredValueRule(100m.Annually(), 900m.Annually(), 0.12m), 123.23m.Annually())
                .Add(new TieredValueRule(900m.Annually(), 1900m.Annually(), 0.22m), 19.6m.Annually())
                .Add(new TieredValueRule(1900m.Annually(), 2900m.Annually(), 0.32m), 5m.Annually())
                .Add(new TieredValueRule(2900m.Annually(), 5000m.Annually(), 0.60m), 0m.Annually());

            var actualResult = _sut.Build();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
