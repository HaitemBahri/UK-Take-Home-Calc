using Moq;
using UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.PensionStrategies;
using UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.StudentLoanStrategies;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategiesTests.StudentLoanStrategiesTests
{
    public class Plan1StudentLoanStrategyTests
    {
        private Plan1StudentLoanStrategy _sut;
        private Mock<ITakeHomeSummaryComposite> _takeHomeSummaryMock = new Mock<ITakeHomeSummaryComposite>();
        private readonly static string _sutName = "Student Loan";

        public static TheoryData<MonetaryValue, ITakeHomeSummaryItem, string> ShouldReturnCorrectTakeHomeSummaryItemTheoryData =>
            new()
            {
                {0m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-9.00]", 0m)).Build(), "Zero Income" },

                {22015m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-9.00]", 0m)).Build(), "Income = Lower Threshold" },

                {42520m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-9.00]", -1845.45m.Annually())).Build(), "Income > Lower Threshold" },
            };

        [Theory]
        [MemberData(nameof(ShouldReturnCorrectTakeHomeSummaryItemTheoryData))]
        public void ShouldReturnCorrectTakeHomeSummaryItem(MonetaryValue value, ITakeHomeSummaryItem expectedResult, string testDataName)
        {
            _sut = new Plan1StudentLoanStrategy(_sutName);
            _takeHomeSummaryMock.Setup(x => x.GetTotal()).Returns(value);

            var actualResult = _sut.CreateTakeHomeSummaryItem(_takeHomeSummaryMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
