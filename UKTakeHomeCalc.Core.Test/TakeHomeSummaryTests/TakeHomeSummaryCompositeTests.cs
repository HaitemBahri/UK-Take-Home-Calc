using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            Assert.True(_sut != _sut2);
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

        public static TheoryData<TakeHomeSummaryComposite, string, string> CompositeWithNoSubLevelsTheoryData
        {
            get
            {
                var composite1 = new TakeHomeSummaryComposite("Sample SUT");
                var expectedResult1 = "Sample SUT";


                return new()
                {
                    { composite1, expectedResult1, "Composite with no sub levels" },
                };
            }
        }

        public static TheoryData<TakeHomeSummaryComposite, string, string> CompositeWith2ItemsTheoryData
        {
            get
            {
                var composite2 = new TakeHomeSummaryComposite("Sample SUT 2");
                composite2.AddValue(new TakeHomeSummaryItem("item - 1", 4500m.Monthly()));
                composite2.AddValue(new TakeHomeSummaryItem("item - 2", 66.666m.Weekly()));
                var ExpectedResult2 = "Sample SUT 2" +
                              "\n\titem - 1 = 4,500.00/Monthly" +
                              "\n\titem - 2 = 66.67/Weekly";

                return new()
                {
                    { composite2, ExpectedResult2, "Composite with 2 items" },
                };
            }
        }

        public static TheoryData<TakeHomeSummaryComposite, string, string> CompositeWith2SubCompositesAndItemsTheoryData
        {
            get
            {
                var composite3 = new TakeHomeSummaryComposite("Sample SUT 3");
                var composite3Sub1 = new TakeHomeSummaryComposite("Sample SUT 3 Sub1");
                composite3Sub1.AddValue(new TakeHomeSummaryItem("item - 1", 4500m.Monthly()));
                composite3Sub1.AddValue(new TakeHomeSummaryItem("item - 2", 66.666m.Weekly()));
                var composite3Sub2 = new TakeHomeSummaryComposite("Sample SUT 3 Sub2");
                composite3Sub2.AddValue(new TakeHomeSummaryItem("item - 7", 66.666m.Weekly()));
                composite3Sub2.AddValue(new TakeHomeSummaryItem("item - 8", 66.666m.Weekly()));
                composite3Sub2.AddValue(new TakeHomeSummaryItem("item - 9", 66.666m.Weekly()));
                composite3.AddValue(composite3Sub1);
                composite3.AddValue(composite3Sub2);
                var expectedResult3 = "Sample SUT 3" +
                              "\n\tSample SUT 3 Sub1" +
                              "\n\t\titem - 1 = 4,500.00/Monthly" +
                              "\n\t\titem - 2 = 66.67/Weekly" +
                              "\n\tSample SUT 3 Sub2" +
                              "\n\t\titem - 7 = 66.67/Weekly" +
                              "\n\t\titem - 8 = 66.67/Weekly" +
                              "\n\t\titem - 9 = 66.67/Weekly";

                return new()
                {
                    { composite3, expectedResult3, "Composite with 2 composites + items" },
                };
            }
        }

        public static TheoryData<TakeHomeSummaryComposite, string, string> CompositeWith5SubLevelsTheoryData
        {
            get
            {
                var composite4 = new TakeHomeSummaryComposite("Sample SUT 4");
                var composite41 = new TakeHomeSummaryComposite("Sample SUT 4.1");
                composite4.AddValue(composite41);
                var composite411 = new TakeHomeSummaryComposite("Sample SUT 4.1.1");
                composite41.AddValue(composite411);
                var composite4111 = new TakeHomeSummaryComposite("Sample SUT 4.1.1.1");
                composite411.AddValue(composite4111);
                var composite41111 = new TakeHomeSummaryComposite("Sample SUT 4.1.1.1.1");
                composite4111.AddValue(composite41111);
                composite41111.AddValue(new TakeHomeSummaryItem("item - 1", 4500m.Monthly()));
                var expectedResult4 = "Sample SUT 4" +
                              "\n\tSample SUT 4.1" +
                              "\n\t\tSample SUT 4.1.1" +
                              "\n\t\t\tSample SUT 4.1.1.1" +
                              "\n\t\t\t\tSample SUT 4.1.1.1.1" +
                              "\n\t\t\t\t\titem - 1 = 4,500.00/Monthly";

                return new()
                {
                    { composite4, expectedResult4, "Composite with 5 sub levels" },
                };
            }
        }

        [Theory]
        [MemberData(nameof(CompositeWithNoSubLevelsTheoryData))]
        [MemberData(nameof(CompositeWith2ItemsTheoryData))]
        [MemberData(nameof(CompositeWith2SubCompositesAndItemsTheoryData))]
        [MemberData(nameof(CompositeWith5SubLevelsTheoryData))]
        public void ShouldReturnCorrectString(TakeHomeSummaryComposite value, string expectedResult, string testDataName)
        {
            var actualResult = value.ToString();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
