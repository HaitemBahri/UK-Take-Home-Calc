using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies.NationalInsuranceStrategy;
using UKTakeHomeCalc.Core.CalculationStrategies.PensionStrategy;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategiesTests.NationalInsuranceStrategiesTests
{
    public class ClassOneNationalInsurnaceStrategiesTests
    {
        private ClassOneNationalInsuranceStrategy _sut;
        private Mock<ITakeHomeSummaryComposite> _takeHomeSummaryMock = new Mock<ITakeHomeSummaryComposite>();
        private readonly static string _sutName = "Pension";

        public static TheoryData<MonetaryValue, MonetaryValue, ITakeHomeSummaryItem, string> ShouldReturnCorrectTakeHomeSummaryItemTheoryData =>
            new()
            {
                {0m.Weekly(), 242m.Weekly(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-12.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-2.00]", 0m)).Build(), "Zero Income" },

                {120m.Weekly(), 242m.Weekly(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-12.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-2.00]", 0m)).Build(), "Income < Lower Threshold" },

                {550m.Weekly(), 242m.Weekly(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-12.00]", -36.96m.Weekly()))
                    .Add(new TakeHomeSummaryItem("@ [%-2.00]", 0m)).Build(), "Lower Threshold < Income < Upper Threshold" },

                {1950.5m.Weekly(), 242m.Weekly(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-12.00]", -87m.Weekly()))
                    .Add(new TakeHomeSummaryItem("@ [%-2.00]", -19.66m.Weekly())).Build(), "Income > Upper Threshold" },
            };

        [Theory]
        [MemberData(nameof(ShouldReturnCorrectTakeHomeSummaryItemTheoryData))]
        public void ShouldReturnCorrectTakeHomeSummaryItem(MonetaryValue value, MonetaryValue freeAllowance, ITakeHomeSummaryItem expectedResult, string testDataName)
        {
            _sut = new ClassOneNationalInsuranceStrategy(_sutName, freeAllowance);
            _takeHomeSummaryMock.Setup(x => x.GetTotal()).Returns(value);

            var actualResult = _sut.CreateTakeHomeSummaryItem(_takeHomeSummaryMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
