using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.SalaryItemCompositeTests
{
    public class SalaryItemCompositeTests_GetTotal
    {
        public static List<object[]> SalaryItems =>
            new List<object[]>() {
                new object[] {
                    new SalaryItem("random name - 1", new MonetaryValue(659.22m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 2", new MonetaryValue(8.5299m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 3", new MonetaryValue(618.369m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(25910.07m , Frequency.WEEKLY)),
                    27196.1889m
                },
                new object[] {
                    new SalaryItem("random name - 1", new MonetaryValue(-569.3m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 2", new MonetaryValue(10m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 3", new MonetaryValue(-95.66m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(0.999m , Frequency.WEEKLY)),
                    -653.961m
                },
                new object[] {
                    new SalaryItem("random name - 1", new MonetaryValue(-60.65m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 2", new MonetaryValue(195.28889m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 3", new MonetaryValue(60.65m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(-195.28889m , Frequency.WEEKLY)),
                    0m
                },
                new object[] {
                    new SalaryItem("random name - 1", new MonetaryValue(-0m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 2", new MonetaryValue(0m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 3", new MonetaryValue(-0m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(0m , Frequency.WEEKLY)),
                    0m
                },
                new object[] {
                    new SalaryItem("random name - 1", new MonetaryValue(-159.666m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 2", new MonetaryValue(0m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 3", new MonetaryValue(-0m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(0m , Frequency.WEEKLY)),
                    -159.666m
                }
            };

        public static List<object[]> SalaryItemsForComposite1 => new List<object[]>() {
            new object[] {
                new SalaryItem[] {
                    new SalaryItem("random name - 1", new MonetaryValue(659.22m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(25910.07m , Frequency.WEEKLY)),
                },
                new SalaryItem[] {
                    new SalaryItem("random name - 1", new MonetaryValue(-569.3m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(0.999m , Frequency.WEEKLY)),
                },
                new SalaryItem[] {
                    new SalaryItem("random name - 1", new MonetaryValue(-60.65m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 2", new MonetaryValue(195.28889m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(-195.28889m , Frequency.WEEKLY)),
                },
                new MonetaryValue[]
                {
                    new MonetaryValue(25940.339m, Frequency.WEEKLY),
                } 
            },
            new object[] {
                new SalaryItem[] {
                    new SalaryItem("random name - 1", new MonetaryValue(2825.2285m , Frequency.MONTHLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(25910.07m , Frequency.WEEKLY)),
                },
                new SalaryItem[] {
                    new SalaryItem("random name - 1", new MonetaryValue(-569.3m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(52.0907m , Frequency.ANNUALLY)),
                },
                new SalaryItem[] {
                    new SalaryItem("random name - 1", new MonetaryValue(-3162.464m , Frequency.ANNUALLY)),
                    new SalaryItem("random name - 2", new MonetaryValue(195.28889m , Frequency.WEEKLY)),
                    new SalaryItem("random name - 4", new MonetaryValue(-195.28889m , Frequency.WEEKLY)),
                },
                new MonetaryValue[]
                {
                    new MonetaryValue(111172.8814m, Frequency.MONTHLY),
                }
            }
        };


        [Theory]
        [MemberData(nameof(SalaryItems))]
        public void ShouldAddValuesAndReturnTotalValue(SalaryItem v1, SalaryItem v2, SalaryItem v3, SalaryItem v4, decimal v5)
        {
            var salaryItems = new List<SalaryItem>();
            var sut = new SalaryItemComposite("salaryitemcomposite name");
            salaryItems.AddRange(new List<SalaryItem>() { v1, v2, v3, v4 });

            foreach (var salaryItem in salaryItems)
            {
                sut.AddValue(salaryItem);
            }

            var expectedResult = new MonetaryValue(v5, Frequency.WEEKLY);
            var actualResult = sut.GetTotal();

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShouldThrowNullException_WhenNullAdded()
        {
            var sut = new SalaryItemComposite("random name");

            Assert.Throws<ArgumentNullException>(() => sut.AddValue(null!));
        }
        [Theory]
        [MemberData(nameof(SalaryItemsForComposite1))]
        public void ShouldAddCompositeValuesAndReturnTotalValue(SalaryItem[] v1, SalaryItem[] v2, SalaryItem[] v3, MonetaryValue[] expectedResult)
        {
            var sut = new SalaryItemComposite("Test salary item composite");

            var salaryItemComposite1 = new SalaryItemComposite("sub comp - 1");
            var salaryItemComposite2 = new SalaryItemComposite("sub comp - 2");
            var salaryItemComposite3 = new SalaryItemComposite("sub comp - 3");

            foreach (var salaryItem1 in v1)
            {
                salaryItemComposite1.AddValue(salaryItem1);
            }

            foreach (var salaryItem2 in v2)
            {
                salaryItemComposite2.AddValue(salaryItem2);
            }

            foreach (var salaryItem3 in v3)
            {
                salaryItemComposite3.AddValue(salaryItem3);
            }

            sut.AddValue(salaryItemComposite1);
            sut.AddValue(salaryItemComposite2);
            sut.AddValue(salaryItemComposite3);

            var actualResult = sut.GetTotal();

            Assert.Equal(expectedResult[0], actualResult);
        }
    }
}
