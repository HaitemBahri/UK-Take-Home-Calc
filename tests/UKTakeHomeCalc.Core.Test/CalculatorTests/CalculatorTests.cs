using Moq;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.TaxStrategies;
using UKTakeHomeCalc.Core.Calculators;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculatorTests
{
    public class CalculatorTests
    {
        private Calculator _sut;
        private readonly Mock<ITakeHomeSummaryComposite> _takeHomeSummaryMock = new();
        private readonly static string _sutName = "Some Calculator";

        public CalculatorTests()
        {
            _takeHomeSummaryMock = new Mock<ITakeHomeSummaryComposite>();
            _takeHomeSummaryMock.Setup(x => x.GetTotal()).Returns(50m.Weekly());
        }

        public static TheoryData<List<ITakeHomeSummaryComposite>, string> ShouldCreateTakeHomeCompositeTheoryData =>
            new()
            {
                { new List<ITakeHomeSummaryComposite>()
                    {
                       { new TakeHomeSummaryCompositeBuilder("Tax")
                        .Add(new TakeHomeSummaryItem("@ [%-20.00]", 154.75m.Monthly()))
                        .Add(new TakeHomeSummaryItem("@ [%-40.00]", 64.00m.Monthly())).Build() },

                       { new TakeHomeSummaryCompositeBuilder(_sutName)
                        .Add(new TakeHomeSummaryCompositeBuilder("Tax")
                            .Add(new TakeHomeSummaryItem("@ [%-20.00]", 154.75m.Monthly()))
                            .Add(new TakeHomeSummaryItem("@ [%-40.00]", 64.00m.Monthly())).Build() )
                        .Build() }
                    }
                    , "Calculator with one Strategies"
                },

                { new List<ITakeHomeSummaryComposite>()
                    {
                       { new TakeHomeSummaryCompositeBuilder("Pension")
                        .Add(new TakeHomeSummaryItem("@ [%-5.00]", 14.05m.Monthly())).Build() },

                       { new TakeHomeSummaryCompositeBuilder("Pension")
                        .Add(new TakeHomeSummaryItem("@ [%-2.50]", 7.025m.Monthly())).Build() },

                       { new TakeHomeSummaryCompositeBuilder(_sutName)
                        .Add(new TakeHomeSummaryCompositeBuilder("Pension")
                            .Add(new TakeHomeSummaryItem("@ [%-5.00]", 14.05m.Monthly())).Build())
                        .Add(new TakeHomeSummaryCompositeBuilder("Pension")
                            .Add(new TakeHomeSummaryItem("@ [%-2.50]", 7.025m.Monthly())).Build())
                        .Build() }
                    }
                    , "Calculator with two Strategies"
                }

            };

        [Theory]
        [MemberData(nameof(ShouldCreateTakeHomeCompositeTheoryData))]
        public void ShouldCreateTakeHomeComposite(List<ITakeHomeSummaryComposite> values, string testDataName)
        {
            var inputValues = values.GetRange(0, values.Count - 1);
            var expectedResult = values.Last();

            var mockedObjects = new List<ICalculationStrategy>();

            foreach (var value in inputValues)
            {
                var mock = new Mock<ICalculationStrategy>();
                mock.Setup(x => x.CreateTakeHomeSummaryItem(_takeHomeSummaryMock.Object))
                    .Returns(value);
                mockedObjects.Add(mock.Object);
            }

            _sut = new Calculator(_sutName, mockedObjects.ToArray());

            var actualResult = _sut.CreateTakeHomeSummaryComposite(_takeHomeSummaryMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WhenCalculationStrategiesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _sut = new Calculator(_sutName, null!));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WhenCalculationStrategiesContainsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _sut = new Calculator(_sutName,
                new ScotlandTaxStrategy("Sco Tax Strategy", FreeAllowances.Tax.StandardTaxFreeAllowance),
                null!,
                new EnglandTaxStrategy("Eng Tax Strategy", FreeAllowances.Tax.StandardTaxFreeAllowance)));
        }
    }
}
