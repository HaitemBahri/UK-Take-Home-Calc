using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.TakeHomeSummaryTests
{
    public class TakeHomeSummaryItemTests
    {
        private TakeHomeSummaryItem _sut;
        private TakeHomeSummaryItem _sutCopy;
        private TakeHomeSummaryItem _sutCopyDifferentName;
        private TakeHomeSummaryItem _sutCopyDifferentValue;

        private TakeHomeSummaryItem _sut2;
        private TakeHomeSummaryItem _sut2Copy;

        public TakeHomeSummaryItemTests()
        {
            _sut = new TakeHomeSummaryItem("TakeHomeSummaryItem SUT Object", 4590m.Monthly());
            _sutCopy = new TakeHomeSummaryItem("TakeHomeSummaryItem SUT Object", 4590m.Monthly());
            _sutCopyDifferentName = new TakeHomeSummaryItem("Different TakeHomeSummaryItem SUT Object", 4590m.Monthly());
            _sutCopyDifferentValue = new TakeHomeSummaryItem("TakeHomeSummaryItem SUT Object", 18990m.Annually());

            _sut2 = new TakeHomeSummaryItem("TakeHomeSummaryItem SUT No 2 Object", 190m.Weekly());
            _sut2Copy = new TakeHomeSummaryItem("TakeHomeSummaryItem SUT No 2 Object", 190m.Weekly());
        }

        [Fact]
        public void ShouldReturnItsOwnValue()
        {
            var value = 4590m.Monthly();

            var actualResult = _sut.GetTotal();

            Assert.Equal(value, actualResult);

        }
        [Fact]
        public void ShouldReturnItsName()
        {
            var expectedResult = "TakeHomeSummaryItem SUT Object";

            var actualResult = _sut.Name;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldFindItself_WhenItsNamePassed()
        {
            var sutName = "TakeHomeSummaryItem SUT Object";
            var expectedResult = _sut;

            var actualResult = _sut.FindValue(sutName);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnNull_WhenNotItsNamePassed()
        {
            var actualResult = _sut.FindValue("Different TakeHomeSummaryItem Object");

            Assert.Null(actualResult);
        }

        [Fact]
        public void ShouldTwoTakeHomeSummaryItemsWithSameNameAndValueBeEqual()
        {
            Assert.Equal(_sutCopy, _sut);
        }

        [Fact]
        public void ShouldTwoTakeHomeSummaryItemsWithSameNameButDifferentValueNotBeEqual()
        {
            Assert.NotEqual(_sutCopyDifferentValue, _sut);
        }

        [Fact]
        public void ShouldTwoTakeHomeSummaryItemsWithDifferentNameButSameValueNotBeEqual()
        {
            Assert.NotEqual(_sutCopyDifferentName, _sut);
        }

        [Fact]
        public void ShouldTwoTakeHomeSummaryItemsWithDifferentNameAndDifferentValueNotBeEqual()
        {
            Assert.NotEqual(_sut2, _sut);
        }

        [Fact]
        public void ShouldReturnCorrectStringValue()
        {
            var expectedResult = "TakeHomeSummaryItem SUT Object = 4,590.00/Monthly";

            var actualResult = _sut.ToString();

            Assert.Equal(expectedResult, actualResult);
        }

    }
}
