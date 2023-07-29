using Moq;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.Calculators;
using UKTakeHomeCalc.Core.CalculatorsHandlers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculatorsHandlersTests
{
    public class CalculatorsHandlerTests
    {
        private CalculatorsHandler _sut;
        private Mock<ITakeHomeSummaryComposite> _takeHomeSummaryMock = new Mock<ITakeHomeSummaryComposite>();

        public CalculatorsHandlerTests()
        {
            _takeHomeSummaryMock = new Mock<ITakeHomeSummaryComposite>();
            _takeHomeSummaryMock.Setup(x => x.GetTotal()).Returns(50m.Weekly());
        }

        public static TheoryData<List<ITakeHomeSummaryComposite>, string> ShouldCreateTakeHomeCompositeTheoryData =>
            new()
            {
                {   new List<ITakeHomeSummaryComposite>()
                    {
                        {    new TakeHomeSummaryCompositeBuilder("Calculator 1")
                                .Add(new TakeHomeSummaryCompositeBuilder("Pension")
                                    .Add(new TakeHomeSummaryItem("@ [%-5.00]", 14.05m.Monthly())).Build())
                                .Add(new TakeHomeSummaryCompositeBuilder("Pension")
                                    .Add(new TakeHomeSummaryItem("@ [%-2.50]", 7.025m.Monthly())).Build())
                                .Build() },

                        {   new TakeHomeSummaryCompositeBuilder("Calculator 2")
                                .Add(new TakeHomeSummaryCompositeBuilder("Tax")
                                    .Add(new TakeHomeSummaryItem("@ [%-20]", 95.55m.Monthly()))
                                    .Add(new TakeHomeSummaryItem("@ [%-40.00]", 4.05m.Monthly())).Build())
                                .Build() },

                        {   new TakeHomeSummaryCompositeBuilder("Calculator 3")
                                .Add(new TakeHomeSummaryCompositeBuilder("National Insurance")
                                    .Add(new TakeHomeSummaryItem("@ [%-12.00]", 13.88m.Monthly()))
                                    .Add(new TakeHomeSummaryItem("@ [%-2.00]", 5.23m.Monthly())).Build())
                                .Build() },

                        {   new TakeHomeSummaryCompositeBuilder("Take-Home Summary")
                                .Add(new TakeHomeSummaryCompositeBuilder("Calculator 1")
                                    .Add(new TakeHomeSummaryCompositeBuilder("Pension")
                                        .Add(new TakeHomeSummaryItem("@ [%-5.00]", 14.05m.Monthly())).Build())
                                    .Add(new TakeHomeSummaryCompositeBuilder("Pension")
                                        .Add(new TakeHomeSummaryItem("@ [%-2.50]", 7.025m.Monthly())).Build())
                                    .Build())
                                .Add(new TakeHomeSummaryCompositeBuilder("Calculator 2")
                                    .Add(new TakeHomeSummaryCompositeBuilder("Tax")
                                        .Add(new TakeHomeSummaryItem("@ [%-20]", 95.55m.Monthly()))
                                        .Add(new TakeHomeSummaryItem("@ [%-40.00]", 4.05m.Monthly())).Build())
                                    .Build())
                                .Add(new TakeHomeSummaryCompositeBuilder("Calculator 3")
                                    .Add(new TakeHomeSummaryCompositeBuilder("National Insurance")
                                        .Add(new TakeHomeSummaryItem("@ [%-12.00]", 13.88m.Monthly()))
                                        .Add(new TakeHomeSummaryItem("@ [%-2.00]", 5.23m.Monthly())).Build())
                                    .Build())
                                .Build()
                        },
                    }
                    , " ..  testDataName .. "
                }

            };

        [Theory]
        [MemberData(nameof(ShouldCreateTakeHomeCompositeTheoryData))]
        public void ShouldCreateTakeHomeSummaryCompositeUsingOneHandler(List<ITakeHomeSummaryComposite> values, string testDataName)
        {
            var inputValues = values.GetRange(0, values.Count - 1);
            var expectedResult = values.Last();

            var mockedObjects = new List<ICalculator>();

            foreach (var value in inputValues)
            {
                var mock = new Mock<ICalculator>();
                mock.Setup(x => x.CreateTakeHomeSummaryComposite(It.IsAny<ITakeHomeSummaryComposite>()))
                    .Returns(value);
                mockedObjects.Add(mock.Object);
            }

            _sut = new CalculatorsHandler(mockedObjects.ToArray());

            var actualResult = new TakeHomeSummaryComposite("Take-Home Summary");
            _sut.Handle(actualResult);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(ShouldCreateTakeHomeCompositeTheoryData))]
        public void ShouldCreateTakeHomeSummaryCompositeUsingMultipleHandler(List<ITakeHomeSummaryComposite> values, string testDataName)
        {
            var inputValues = values.GetRange(0, values.Count - 1);
            var expectedResult = values.Last();

            var mockedObjects = new List<ICalculator>();
            var calculatorsHandlers = new List<CalculatorsHandler>();

            for (int i = 0; i < inputValues.Count; i++)
            {
                var mock = new Mock<ICalculator>();
                mock.Setup(x => x.CreateTakeHomeSummaryComposite(It.IsAny<ITakeHomeSummaryComposite>()))
                    .Returns(inputValues[i]);
                mockedObjects.Add(mock.Object);

                calculatorsHandlers.Add(new CalculatorsHandler(mock.Object));
            }

            for (int i = 0; i < inputValues.Count -1; i++)
            {
                calculatorsHandlers[i].SetNext(calculatorsHandlers[i + 1]);
            }

            _sut = calculatorsHandlers[0];

            var actualResult = new TakeHomeSummaryComposite("Take-Home Summary");
            _sut.Handle(actualResult);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
