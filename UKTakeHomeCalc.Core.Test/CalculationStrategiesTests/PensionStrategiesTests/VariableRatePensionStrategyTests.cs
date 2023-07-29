using Moq;
using UKTakeHomeCalc.Core.CalculationStrategies.PensionStrategies;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategiesTests.PensionStrategiesTests
{
    public class VariableRatePensionStrategyTests
    {
        private VariableRatePensionStrategy _sut;
        private Mock<ITakeHomeSummaryComposite> _takeHomeSummaryMock = new Mock<ITakeHomeSummaryComposite>();
        private readonly static string _sutName = "Pension";

        public static TheoryData<MonetaryValue, decimal, ITakeHomeSummaryItem, string> ShouldReturnCorrectTakeHomeSummaryItemTheoryData =>
            new()
            {
                {0m.Annually(), 0.05m, new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-5.00]", 0m)).Build(), "Zero Income" },

                {3000m.Annually(), 0.05m, new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-5.00]", 0m)).Build(), "Income < Lower Limit" },

                {35000m.Annually(), 0.05m, new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-5.00]", -1438m.Annually())).Build(), "Lower Limit < Income < Upper Limit" },

                {72500m.Annually(), 0.05m, new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-5.00]", -2201.5m.Annually())).Build(), "Income > Upper Limit" },
            };

        [Theory]
        [MemberData(nameof(ShouldReturnCorrectTakeHomeSummaryItemTheoryData))]
        public void ShouldReturnCorrectTakeHomeSummaryItem(MonetaryValue value, decimal percentage, ITakeHomeSummaryItem expectedResult, string testDataName)
        {
            _sut = new VariableRatePensionStrategy(_sutName, percentage, FreeAllowances.Pension.StandardPensionFreeAllowance);
            _takeHomeSummaryMock.Setup(x => x.GetTotal()).Returns(value);

            var actualResult = _sut.CreateTakeHomeSummaryItem(_takeHomeSummaryMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
