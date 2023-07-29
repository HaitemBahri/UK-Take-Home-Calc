using Moq;
using UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategies;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategiesTests.TaxStrategiesTests
{
    public class EnglandTaxStrategyTests
    {
        private EnglandTaxStrategy _sut;
        private Mock<ITakeHomeSummaryComposite> _takeHomeSummaryMock = new Mock<ITakeHomeSummaryComposite>();
        private readonly static string _sutName = "Tax";

        public static TheoryData<MonetaryValue, MonetaryValue, ITakeHomeSummaryItem, string> ShouldReturnCorrectTakeHomeSummaryItemTheoryData =>
            new()
            {
                {0m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-40.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-45.00]", 0m)).Build(), "Zero Income" },

                {2570m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-40.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-45.00]", 0m)).Build(), "Income < FreeAllowance" },

                {12570m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-40.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-45.00]", 0m)).Build(), "Income = FreeAllowance" },

                {27770m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", -3040m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-40.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-45.00]", 0m)).Build(), "Income @ Basic Rate" },

                {77070m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", -7540m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-40.00]", -10720m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-45.00]", 0m)).Build(), "Income @ Higher Rate" },

                {119820m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", -7540m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-40.00]", -31784m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-45.00]", 0m)).Build(), "Income Between Income Limit & Additional Rate" },

                {251000m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", -7540m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-40.00]", -34976m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-45.00]", -56637m.Annually())).Build(), "Income @ Additional Rate" },
            };

        [Theory]
        [MemberData(nameof(ShouldReturnCorrectTakeHomeSummaryItemTheoryData))]
        public void ShouldReturnCorrectTakeHomeSummaryItem(MonetaryValue value, MonetaryValue freeAllowance, ITakeHomeSummaryItem expectedResult, string testDataName)
        {
            _sut = new EnglandTaxStrategy(_sutName, freeAllowance);
            _takeHomeSummaryMock.Setup(x => x.GetTotal()).Returns(value);

            var actualResult = _sut.CreateTakeHomeSummaryItem(_takeHomeSummaryMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
