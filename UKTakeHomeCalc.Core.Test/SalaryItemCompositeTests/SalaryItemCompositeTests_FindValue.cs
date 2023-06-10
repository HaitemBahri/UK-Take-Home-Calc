using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.SalaryItemCompositeTests
{
    public class SalaryItemCompositeTests_FindValue
    {
        [Fact]
        public void ShouldFindGivenValue()
        {
            var sut = new SalaryItemNode("Main Composite item");
            var salaryItem1 = new SalaryItem("Sub SalaryBreakdown Item No 1", new MonetaryValue(65.33m, Frequency.Weekly));
            var salaryItem2 = new SalaryItem("Sub SalaryBreakdown Item No 2", new MonetaryValue(-18.55m, Frequency.Weekly));
            var salaryItem3 = new SalaryItem("Sub SalaryBreakdown Item No 3", new MonetaryValue(45000.00m, Frequency.Annually));

            sut.AddValue(salaryItem1);
            sut.AddValue(salaryItem2);
            sut.AddValue(salaryItem3);

            var actualResult = sut.FindValue("Sub SalaryBreakdown Item No 3");

            Assert.Equal(salaryItem3, actualResult);

        }
        [Fact]
        public void ShouldFindGivenValueFromNestedCompositeObjects()
        {
            var sut = new SalaryItemNode("Main Composite item");

            var salaryItemComposite1 = new SalaryItemNode("Composite item 1");
            var salaryItemComposite2 = new SalaryItemNode("Composite item 2");
            var salaryItemComposite3 = new SalaryItemNode("Composite item 3");
            var salaryItem1 = new SalaryItem("Sub SalaryBreakdown Item No 1", new MonetaryValue(6555.33m, Frequency.Monthly));
            var salaryItem2 = new SalaryItem("Sub SalaryBreakdown Item No 2", new MonetaryValue(65.33m, Frequency.Weekly));
            var salaryItem3 = new SalaryItem("Sub SalaryBreakdown Item No 3", new MonetaryValue(-18.55m, Frequency.Weekly));
            var salaryItem4 = new SalaryItem("Sub SalaryBreakdown Item No 4", new MonetaryValue(-218.55m, Frequency.Weekly));
            var salaryItem5 = new SalaryItem("Sub SalaryBreakdown Item No 5", new MonetaryValue(45000.00m, Frequency.Annually));
            var salaryItem6 = new SalaryItem("Sub SalaryBreakdown Item No 6", new MonetaryValue(450.00m, Frequency.Monthly));

            salaryItemComposite1.AddValue(salaryItem1);
            salaryItemComposite2.AddValue(salaryItem2);
            salaryItemComposite2.AddValue(salaryItem3);
            salaryItemComposite2.AddValue(salaryItem4);
            salaryItemComposite3.AddValue(salaryItem5);
            salaryItemComposite3.AddValue(salaryItem6);

            sut.AddValue(salaryItemComposite1);
            sut.AddValue(salaryItemComposite2);
            sut.AddValue(salaryItemComposite3);

            var actualResult = sut.FindValue("Sub SalaryBreakdown Item No 4");

            Assert.Equal(salaryItem4, actualResult);


        }
        [Fact]
        public void ShouldReturnNullWhenNoValuePresent()
        {
            var sut = new SalaryItemNode("Main Composite item");
            var salaryItem1 = new SalaryItem("Sub SalaryBreakdown Item No 1", new MonetaryValue(65.33m, Frequency.Weekly));
            var salaryItem2 = new SalaryItem("Sub SalaryBreakdown Item No 2", new MonetaryValue(-18.55m, Frequency.Weekly));
            var salaryItem3 = new SalaryItem("Sub SalaryBreakdown Item No 3", new MonetaryValue(45000.00m, Frequency.Annually));

            sut.AddValue(salaryItem1);
            sut.AddValue(salaryItem2);
            sut.AddValue(salaryItem3);

            var actualResult = sut.FindValue("No Item");

            Assert.Null(actualResult);
        }
    }
}
