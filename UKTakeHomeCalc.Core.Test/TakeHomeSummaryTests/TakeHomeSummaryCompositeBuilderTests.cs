using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using UKTakeHomeCalc.Core.TieredValueCalculators;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.TakeHomeSummaryTests
{
    public class TakeHomeSummaryCompositeBuilderTests
    {
        private TakeHomeSummaryCompositeBuilder _sut;

        public TakeHomeSummaryCompositeBuilderTests()
        {
            _sut = new TakeHomeSummaryCompositeBuilder("TakeHomeSummaryCompositeBuilder SUT");
        }

        [Fact]
        public void ShouldBuildCompositeWithNoSubItems()
        {
            var expectedResult = new TakeHomeSummaryComposite("TakeHomeSummaryCompositeBuilder SUT");
            var actualResult = _sut.Build();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldBuildCompositeWithSubItemsUsingNameAndValueForItems()
        {
            _sut.Add("Sub Item 1", 900.33m.Weekly());
            _sut.Add("Sub Item 2", 12663.10m.Annually());

            var expectedResult = new TakeHomeSummaryComposite("TakeHomeSummaryCompositeBuilder SUT");
            expectedResult.AddValue(new TakeHomeSummaryItem("Sub Item 1", 900.33m.Weekly()));
            expectedResult.AddValue(new TakeHomeSummaryItem("Sub Item 2", 12663.10m.Annually()));

            var actualResult = _sut.Build();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldBuildCompositeWithSubItemsUsingTakeHomeSummaryItem()
        {
            _sut.Add(new TakeHomeSummaryItem("Sub Item 1", 900.33m.Weekly()));
            _sut.Add(new TakeHomeSummaryItem("Sub Item 2", 12663.10m.Annually()));

            var expectedResult = new TakeHomeSummaryComposite("TakeHomeSummaryCompositeBuilder SUT");
            expectedResult.AddValue(new TakeHomeSummaryItem("Sub Item 1", 900.33m.Weekly()));
            expectedResult.AddValue(new TakeHomeSummaryItem("Sub Item 2", 12663.10m.Annually()));

            var actualResult = _sut.Build();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldBuildCompositeWithSubComposites()
        {
            var subComposite1 = new TakeHomeSummaryComposite("Sub Composite 1");
            var subComposite2 = new TakeHomeSummaryComposite("Sub Composite 2");
            var subComposite3 = new TakeHomeSummaryComposite("Sub Composite 3");

            subComposite1.AddValue(new TakeHomeSummaryItem("Sub Item 1.1", 900.33m.Weekly()));
            subComposite1.AddValue(new TakeHomeSummaryItem("Sub Item 1.2", 453.555m.Weekly()));

            subComposite2.AddValue(new TakeHomeSummaryItem("Sub Item 2.1", 6000m.Annually()));

            subComposite3.AddValue(new TakeHomeSummaryItem("Sub Item 3.1", 650m.Monthly()));
            subComposite3.AddValue(new TakeHomeSummaryItem("Sub Item 3.2", 53.999m.Weekly()));

            _sut.Add(subComposite1, subComposite2, subComposite3);

            var expectedResult = new TakeHomeSummaryComposite("TakeHomeSummaryCompositeBuilder SUT");
            var expectedSubComposite1 = new TakeHomeSummaryComposite("Sub Composite 1");
            var expectedSubComposite2 = new TakeHomeSummaryComposite("Sub Composite 2");
            var expectedSubComposite3 = new TakeHomeSummaryComposite("Sub Composite 3");

            expectedSubComposite1.AddValue(new TakeHomeSummaryItem("Sub Item 1.1", 900.33m.Weekly()));
            expectedSubComposite1.AddValue(new TakeHomeSummaryItem("Sub Item 1.2", 453.555m.Weekly()));

            expectedSubComposite2.AddValue(new TakeHomeSummaryItem("Sub Item 2.1", 6000m.Annually()));

            expectedSubComposite3.AddValue(new TakeHomeSummaryItem("Sub Item 3.1", 650m.Monthly()));
            expectedSubComposite3.AddValue(new TakeHomeSummaryItem("Sub Item 3.2", 53.999m.Weekly()));

            expectedResult.AddValue(expectedSubComposite1);
            expectedResult.AddValue(expectedSubComposite2);
            expectedResult.AddValue(expectedSubComposite3);

            var actualResult = _sut.Build();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldBuildCompositeWithSubTieredValueResults()
        {
            var rule1 = new TieredValueRule(600m.Monthly(), 3000m.Monthly(), 0.25m);
            var result1 = new TieredValueResult(rule1, 99m.Monthly());
            var rule2 = new TieredValueRule(600m.Monthly(), 3000m.Monthly(), 0.425999m);
            var result2 = new TieredValueResult(rule2, 51.055m.Monthly());
            var rule3 = new TieredValueRule(600m.Monthly(), 3000m.Monthly(), 0.44m);
            var result3 = new TieredValueResult(rule3, 23.88m.Monthly());

            _sut.Add(result1, result2, result3);

            var expectedResult = new TakeHomeSummaryComposite("TakeHomeSummaryCompositeBuilder SUT");
            expectedResult.AddValue(new TakeHomeSummaryItem("@ [%25.00]", 99m.Monthly()));
            expectedResult.AddValue(new TakeHomeSummaryItem("@ [%42.60]", 51.055m.Monthly()));
            expectedResult.AddValue(new TakeHomeSummaryItem("@ [%44.00]", 23.88m.Monthly()));

            var actualResult = _sut.Build();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
