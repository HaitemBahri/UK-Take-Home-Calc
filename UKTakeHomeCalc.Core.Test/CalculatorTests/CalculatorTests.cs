using Moq;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.Calculators;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.CalculatorTests
{
    public class CalculatorTests
    {
        private Calculator _sut;

        private Mock<ITakeHomeSummaryComposite> _calculatorNode;

        private Mock<ICalculationStrategy> _calculationStrategyMock1;
        private Mock<ICalculationStrategy> _calculationStrategyMock2;
        private Mock<ICalculationStrategy> _calculationStrategyMock3;

        private Mock<ITakeHomeSummaryComposite> _takeHomeSummeryMock;

        public CalculatorTests()
        {
            _calculationStrategyMock1 = new Mock<ICalculationStrategy>();
            _calculationStrategyMock2 = new Mock<ICalculationStrategy>();
            _calculationStrategyMock3 = new Mock<ICalculationStrategy>();

            _takeHomeSummeryMock = new Mock<ITakeHomeSummaryComposite>();
            _takeHomeSummeryMock.Setup(x => x.GetTotal()).Returns(50m.Weekly());

            _calculatorNode = new Mock<ITakeHomeSummaryComposite>();

            _sut = new Calculator(_calculatorNode.Object, new List<ICalculationStrategy>()
                                  {
                                      _calculationStrategyMock1.Object,
                                      _calculationStrategyMock2.Object,
                                      _calculationStrategyMock3.Object,
                                  });

            _calculationStrategyMock1.Setup(x => x.CreateSalaryItem(It.IsAny<ITakeHomeSummaryComposite>()))
                .Returns(new TakeHomeSummaryItem("salaryItem test 1", 550.0m.Weekly()));
            _calculationStrategyMock2.Setup(x => x.CreateSalaryItem(It.IsAny<ITakeHomeSummaryComposite>()))
                .Returns(new TakeHomeSummaryItem("salaryItem test 2", 905.0m.Weekly()));
            _calculationStrategyMock3.Setup(x => x.CreateSalaryItem(It.IsAny<ITakeHomeSummaryComposite>()))
                .Returns(new TakeHomeSummaryItem("salaryItem test 3", 10.0m.Weekly()));

        }

        [Fact]
        public void CreateSalaryItemNode_ShouldRetrieveSalaryItemFromCalculationStrategy()
        {

            var x = _sut.CreateSalaryItemNode(_takeHomeSummeryMock.Object);

            _calculationStrategyMock1.Verify(x => x.CreateSalaryItem(It.IsAny<ITakeHomeSummaryComposite>()), Times.Exactly(1));
            _calculationStrategyMock2.Verify(x => x.CreateSalaryItem(It.IsAny<ITakeHomeSummaryComposite>()), Times.Exactly(1));
            _calculationStrategyMock3.Verify(x => x.CreateSalaryItem(It.IsAny<ITakeHomeSummaryComposite>()), Times.Exactly(1));
        }
        [Fact]
        public void CreateSalaryItemNode_ShouldAddSalaryItemsToItsCalculatorNode()
        {
            var x = _sut.CreateSalaryItemNode(_takeHomeSummeryMock.Object);

            _calculatorNode.Verify(x => x.AddValue(It.IsAny<ITakeHomeSummaryItem>()), Times.Exactly(3));
        }
        [Fact]
        public void ShouldReturnItsNameNodeWithSalaryItems()
        {
            var expectedResult = _calculatorNode.Object;

            var actualValue = _sut.CreateSalaryItemNode(_takeHomeSummeryMock.Object);

            Assert.Equal(expectedResult, actualValue);
        }
    }
}
