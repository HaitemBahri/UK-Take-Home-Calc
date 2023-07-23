using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies.IncomeStrategies;
using UKTakeHomeCalc.Core.CalculationStrategies.TaxStrategy;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculationStrategiesTests.TaxStrategiesTests
{
    public class ScotlandTaxStrategyTests
    {
        private ScotlandTaxStrategy _sut;
        private Mock<ITakeHomeSummaryComposite> _takeHomeSummaryMock = new Mock<ITakeHomeSummaryComposite>();
        private readonly static string _sutName = "Tax";

        public static TheoryData<MonetaryValue, MonetaryValue, ITakeHomeSummaryItem, string> ShouldReturnCorrectTakeHomeSummaryItemTheoryData =>
            new()
            {
                {0m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-19.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-21.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-42.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-47.00]", 0m)).Build(), "Zero Income" },

                {5000m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-19.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-21.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-42.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-47.00]", 0m)).Build(), "Income < FreeAllowance" },

                {12570m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-19.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-21.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-42.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-47.00]", 0m)).Build(), "Income = FreeAllowance" },

                {14070m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-19.00]", -285m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-21.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-42.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-47.00]", 0m)).Build(), "Income @ Starter Rate" },

                {22070m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-19.00]", -410.78m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", -1467.6m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-21.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-42.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-47.00]", 0m)).Build(), "Income @ Basic Rate" },

                {37570m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-19.00]", -410.78m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", -2191.2m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-21.00]", -2495.22m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-42.00]", 0m))
                    .Add(new TakeHomeSummaryItem("@ [%-47.00]", 0m)).Build(), "Income @ Intermediate Rate" },

                {67570m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-19.00]", -410.78m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", -2191.2m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-21.00]", -3774.54m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-42.00]", -10041.36m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-47.00]", 0m.Annually())).Build(), "Income @ Higher Rate" },

                {119820m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-19.00]", -410.78m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", -2191.2m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-21.00]", -3774.54m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-42.00]", -36148.56m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-47.00]", 0m.Annually())).Build(), "Income Between Income Limit & Additional Rate" },

                {157570.5m.Annually(), 12570m.Annually(), new TakeHomeSummaryCompositeBuilder(_sutName)
                    .Add(new TakeHomeSummaryItem("@ [%-19.00]", -410.78m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-20.00]", -2191.2m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-21.00]", -3774.54m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-42.00]", -39500.16m.Annually()))
                    .Add(new TakeHomeSummaryItem("@ [%-47.00]", -15242.34m.Annually())).Build(), "Income @ Top Rate" },
            };

        [Theory]
        [MemberData(nameof(ShouldReturnCorrectTakeHomeSummaryItemTheoryData))]
        public void ShouldReturnCorrectTakeHomeSummaryItem(MonetaryValue value, MonetaryValue freeAllowance, ITakeHomeSummaryItem expectedResult, string testDataName)
        {
            _sut = new ScotlandTaxStrategy(_sutName, freeAllowance);
            _takeHomeSummaryMock.Setup(x => x.GetTotal()).Returns(value);

            var actualResult = _sut.CreateTakeHomeSummaryItem(_takeHomeSummaryMock.Object);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
