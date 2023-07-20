using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.TakeHomeSummaryTests
{
    public class TakeHomeSummaryBuilderTests
    {
        private TakeHomeSummaryBuilder _sut;

        public TakeHomeSummaryBuilderTests()
        {
            _sut = new TakeHomeSummaryBuilder("TakeHomeSummaryBuilder SUT");
            _sut.Build();
        }

        [Fact]
        public void ShouldBuildCompositeWithNoSubItems()
        {
            var expectedResult = new TakeHomeSummaryComposite("TakeHomeSummaryBuilder SUT");
            var actualResult = _sut.Build();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldBuildCompositeWithSubItems()
        {
            _sut.Add("Sub Item 1", 900.33m.Weekly());
            _sut.Add("Sub Item 2", 12663.10m.Annually());

            var expectedResult = new TakeHomeSummaryComposite("TakeHomeSummaryBuilder SUT");
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

            var expectedResult = new TakeHomeSummaryComposite("TakeHomeSummaryBuilder SUT");
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
    }
}
