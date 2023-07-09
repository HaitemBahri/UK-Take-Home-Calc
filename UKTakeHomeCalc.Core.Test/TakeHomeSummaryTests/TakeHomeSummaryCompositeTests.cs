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
    public class TakeHomeSummaryCompositeTests
    {
        private TakeHomeSummaryComposite _sut;
        private TakeHomeSummaryComposite _sutCopy;
        private TakeHomeSummaryComposite _sut2;

        private TakeHomeSummaryComposite _composite1;
        private TakeHomeSummaryComposite _composite2;
        private TakeHomeSummaryComposite _composite3;

        private TakeHomeSummaryItem _item1;
        private TakeHomeSummaryItem _item2;
        private TakeHomeSummaryItem _item3;
        private TakeHomeSummaryItem _item4;
        private TakeHomeSummaryItem _item5;
        private TakeHomeSummaryItem _item6;

        private TakeHomeSummaryItem _item1Copy;
        private TakeHomeSummaryItem _item2Copy;
        private TakeHomeSummaryItem _item3Copy;
        private TakeHomeSummaryItem _item4Copy;
        private TakeHomeSummaryItem _item5Copy;
        private TakeHomeSummaryItem _item6Copy;

        public TakeHomeSummaryCompositeTests()
        {
            _sut = new TakeHomeSummaryComposite("TakeHomeSummaryComposite SUT Object");
            _sutCopy = new TakeHomeSummaryComposite("TakeHomeSummaryComposite SUT Object");
            _sut2 = new TakeHomeSummaryComposite("TakeHomeSummaryComposite SUT Object No 2");

            _composite1 = new TakeHomeSummaryComposite("TakeHomeSummaryComposite No 1");
            _composite2 = new TakeHomeSummaryComposite("TakeHomeSummaryComposite No 2");
            _composite3 = new TakeHomeSummaryComposite("TakeHomeSummaryComposite No 3");

            _item1 = new TakeHomeSummaryItem("TakeHomeSummaryItem No 1", 65.33m.Weekly());
            _item1Copy = new TakeHomeSummaryItem("TakeHomeSummaryItem No 1", 65.33m.Weekly());

            _item2 = new TakeHomeSummaryItem("TakeHomeSummaryItem No 2", -18.55m.Weekly());
            _item2Copy = new TakeHomeSummaryItem("TakeHomeSummaryItem No 2", -18.55m.Weekly());

            _item3 = new TakeHomeSummaryItem("TakeHomeSummaryItem No 3", 45000.00m.Annually());
            _item3Copy = new TakeHomeSummaryItem("TakeHomeSummaryItem No 3", 45000.00m.Annually());

            _item4 = new TakeHomeSummaryItem("TakeHomeSummaryItem No 4", -218.55m.Monthly());
            _item4Copy = new TakeHomeSummaryItem("TakeHomeSummaryItem No 4", -218.55m.Monthly());

            _item5 = new TakeHomeSummaryItem("TakeHomeSummaryItem No 5", 450.00m.Weekly());
            _item5Copy = new TakeHomeSummaryItem("TakeHomeSummaryItem No 5", 450.00m.Weekly());

            _item6 = new TakeHomeSummaryItem("TakeHomeSummaryItem No 6", 12.5m.Monthly());
            _item6Copy = new TakeHomeSummaryItem("TakeHomeSummaryItem No 6", 12.5m.Monthly());
        }
        [Fact]
        public void ShouldReturnItsName()
        {
            var expectedResult = "TakeHomeSummaryComposite SUT Object";

            var actualResult = _sut.Name;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldFindGivenValue_WhenNameExists()
        {
            var expectedResult = _item4;

            _composite1.AddValue(_item1);
            _composite2.AddValue(_item2);
            _composite2.AddValue(_item3);
            _composite2.AddValue(_item4);
            _composite3.AddValue(_item5);
            _composite3.AddValue(_item6);

            _sut.AddValue(_composite1);
            _sut.AddValue(_composite2);
            _sut.AddValue(_composite3);
            _sut.AddValue(_item4Copy);

            var actualResult = _sut.FindValue("TakeHomeSummaryItem No 4");

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShouldReturnNull_WhenNameNoExists()
        {
            _composite1.AddValue(_item1);
            _composite2.AddValue(_item2);
            _composite2.AddValue(_item3);
            _composite3.AddValue(_item5);

            _sut.AddValue(_composite1);
            _sut.AddValue(_composite2);
            _sut.AddValue(_composite3);

            var actualResult = _sut.FindValue("TakeHomeSummaryItem No 4");

            Assert.Null(actualResult);
        }

        [Fact]
        public void ShouldReturnTotalValue()
        {
            var expectedResult = 68244.50m.Annually();

            _composite1.AddValue(_item1);
            _composite2.AddValue(_item2);
            _composite2.AddValue(_item3);
            _composite3.AddValue(_item5);

            _sut.AddValue(_composite1);
            _sut.AddValue(_composite2);
            _sut.AddValue(_composite3);
            _sut.AddValue(_item4);

            var actualResult = _sut.GetTotal();
            
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShouldThrowNullException_WhenNullAdded()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.AddValue(null!));
        }

        [Fact]
        public void ShouldTwoValuesBeEqual_WhenNameAndItemsAreSame()
        {
            _sut.AddValue(_item1);
            _sut.AddValue(_item2);
            _sut.AddValue(_item3);

            _sutCopy.AddValue(_item1Copy);
            _sutCopy.AddValue(_item2Copy);
            _sutCopy.AddValue(_item3Copy);

            Assert.True(_sut == _sutCopy);
        }

        [Fact]
        public void ShouldTwoValuesNotBeEqual_WhenNameIsSameButItemsAreDifferent()
        {
            _sut.AddValue(_item1);
            _sut.AddValue(_item2);
            _sut.AddValue(_item3);

            _sutCopy.AddValue(_item1Copy);
            _sutCopy.AddValue(_item3Copy);
            _sutCopy.AddValue(_item2Copy);

            Assert.True(_sut != _sutCopy);
        }

        [Fact]
        public void ShouldTwoValuesNotBeEqual_WhenItemsAreSameButNameIsDifferent()
        {
            _sut.AddValue(_item1);
            _sut.AddValue(_item2);
            _sut.AddValue(_item3);

            _sut2.AddValue(_item1Copy);
            _sut2.AddValue(_item2Copy);
            _sut2.AddValue(_item3Copy);

            Assert.True(_sut != _sutCopy);
        }

        [Fact]
        public void ShouldTwoValuesNotBeEqual_WhenNameAndItemsAreDifferent()
        {
            _sut.AddValue(_item1);
            _sut.AddValue(_item2);
            _sut.AddValue(_item3);

            _sut2.AddValue(_item4Copy);
            _sut2.AddValue(_item5Copy);
            _sut2.AddValue(_item6Copy);

            Assert.True(_sut != _sutCopy);
        }
    }
}
