using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.SalaryItemCompositeTests
{
    public class SalaryItemCompositeTests_GetTotal
    {
        public static List<object[]> SalaryItems =>
            new List<object[]>() {
                new object[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(659.22m , Frequency.Weekly)),
                    new SalaryItem("random _name - 2", new MonetaryValue(8.5299m , Frequency.Weekly)),
                    new SalaryItem("random _name - 3", new MonetaryValue(618.369m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(25910.07m , Frequency.Weekly)),
                    27196.1889m
                },
                new object[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(-569.3m , Frequency.Weekly)),
                    new SalaryItem("random _name - 2", new MonetaryValue(10m , Frequency.Weekly)),
                    new SalaryItem("random _name - 3", new MonetaryValue(-95.66m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(0.999m , Frequency.Weekly)),
                    -653.961m
                },
                new object[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(-60.65m , Frequency.Weekly)),
                    new SalaryItem("random _name - 2", new MonetaryValue(195.28889m , Frequency.Weekly)),
                    new SalaryItem("random _name - 3", new MonetaryValue(60.65m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(-195.28889m , Frequency.Weekly)),
                    0m
                },
                new object[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(-0m , Frequency.Weekly)),
                    new SalaryItem("random _name - 2", new MonetaryValue(0m , Frequency.Weekly)),
                    new SalaryItem("random _name - 3", new MonetaryValue(-0m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(0m , Frequency.Weekly)),
                    0m
                },
                new object[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(-159.666m , Frequency.Weekly)),
                    new SalaryItem("random _name - 2", new MonetaryValue(0m , Frequency.Weekly)),
                    new SalaryItem("random _name - 3", new MonetaryValue(-0m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(0m , Frequency.Weekly)),
                    -159.666m
                }
            };

        public static List<object[]> SalaryItemsForComposite1 => new List<object[]>() {
            new object[] {
                new SalaryItem[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(659.22m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(25910.07m , Frequency.Weekly)),
                },
                new SalaryItem[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(-569.3m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(0.999m , Frequency.Weekly)),
                },
                new SalaryItem[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(-60.65m , Frequency.Weekly)),
                    new SalaryItem("random _name - 2", new MonetaryValue(195.28889m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(-195.28889m , Frequency.Weekly)),
                },
                new MonetaryValue[]
                {
                    new MonetaryValue(25940.339m, Frequency.Weekly),
                } 
            },
            new object[] {
                new SalaryItem[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(2825.2285m , Frequency.Monthly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(25910.07m , Frequency.Weekly)),
                },
                new SalaryItem[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(-569.3m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(52.0907m , Frequency.Annually)),
                },
                new SalaryItem[] {
                    new SalaryItem("random _name - 1", new MonetaryValue(-3162.464m , Frequency.Annually)),
                    new SalaryItem("random _name - 2", new MonetaryValue(195.28889m , Frequency.Weekly)),
                    new SalaryItem("random _name - 4", new MonetaryValue(-195.28889m , Frequency.Weekly)),
                },
                new MonetaryValue[]
                {
                    new MonetaryValue(111172.8814m, Frequency.Monthly),
                }
            }
        };


        [Theory]
        [MemberData(nameof(SalaryItems))]
        public void ShouldAddValuesAndReturnTotalValue(SalaryItem v1, SalaryItem v2, SalaryItem v3, SalaryItem v4, decimal v5)
        {
            var salaryItems = new List<SalaryItem>();
            var sut = new SalaryItemNode("salaryitemcomposite _name");
            salaryItems.AddRange(new List<SalaryItem>() { v1, v2, v3, v4 });

            foreach (var salaryItem in salaryItems)
            {
                sut.AddValue(salaryItem);
            }

            var expectedResult = new MonetaryValue(v5, Frequency.Weekly);
            var actualResult = sut.GetTotal();

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShouldThrowNullException_WhenNullAdded()
        {
            var sut = new SalaryItemNode("random _name");

            Assert.Throws<ArgumentNullException>(() => sut.AddValue(null!));
        }
        [Theory]
        [MemberData(nameof(SalaryItemsForComposite1))]
        public void ShouldAddCompositeValuesAndReturnTotalValue(SalaryItem[] v1, SalaryItem[] v2, SalaryItem[] v3, MonetaryValue[] expectedResult)
        {
            var sut = new SalaryItemNode("Test salary item composite");

            var salaryItemComposite1 = new SalaryItemNode("sub comp - 1");
            var salaryItemComposite2 = new SalaryItemNode("sub comp - 2");
            var salaryItemComposite3 = new SalaryItemNode("sub comp - 3");

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
