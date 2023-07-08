using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Calculators;
using UKTakeHomeCalc.Core.CalculatorsHandlers;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.CalculatorsHandlerTests
{
    public class CalculatorsHandlerTests
    {
        private CalculatorsHandler _sut;

        private Mock<ICalculatorsHandler> _calculatorsHandlerMock;

        private Mock<ICalculator> _calculatorMock1;
        private Mock<ICalculator> _calculatorMock2;
        private Mock<ICalculator> _calculatorMock3;

        private Mock<ITakeHomeSummaryComposite> _takeHomeSummeryMock;
        private List<ICalculator> _calculatorsList;

        public CalculatorsHandlerTests()
        {
            _calculatorsHandlerMock = new Mock<ICalculatorsHandler>();

            _calculatorMock1 = new Mock<ICalculator>();
            _calculatorMock2 = new Mock<ICalculator>();
            _calculatorMock3 = new Mock<ICalculator>();

            _takeHomeSummeryMock = new Mock<ITakeHomeSummaryComposite>();
            _takeHomeSummeryMock.Setup(x => x.GetTotal()).Returns(50m.Weekly());
            
            _calculatorMock1.Setup(x => x.CreateSalaryItemNode(null!)).Returns(new TakeHomeSummaryComposite("some node 1"));
            _calculatorMock2.Setup(x => x.CreateSalaryItemNode(null!)).Returns(new TakeHomeSummaryComposite("some node 2"));
            _calculatorMock3.Setup(x => x.CreateSalaryItemNode(null!)).Returns(new TakeHomeSummaryComposite("some node 3"));

            _calculatorsList = new List<ICalculator>() { 
                _calculatorMock1.Object,
                _calculatorMock2.Object,
                _calculatorMock3.Object
            };

            _sut = new CalculatorsHandler(_calculatorsList);
        }
        [Fact]
        public void Handle_ShouldRetrieveSalaryItemsFromCalculators()
        {
            _sut.Handle(_takeHomeSummeryMock.Object);

            _calculatorMock1.Verify(x => x.CreateSalaryItemNode(It.IsAny<ITakeHomeSummaryComposite>()), Times.Once);
            _calculatorMock2.Verify(x => x.CreateSalaryItemNode(It.IsAny<ITakeHomeSummaryComposite>()), Times.Once);
            _calculatorMock3.Verify(x => x.CreateSalaryItemNode(It.IsAny<ITakeHomeSummaryComposite>()), Times.Once);
        }

        [Fact]
        public void Handle_ShouldAddSalaryItemsToTakeHomeSummery()
        {
            _sut.Handle(_takeHomeSummeryMock.Object);

            _takeHomeSummeryMock.Verify(x => x.AddValue(It.IsAny<ITakeHomeSummaryItem>()), Times.Exactly(_calculatorsList.Count));
        }
        [Fact]
        public void Handle_ShouldHandleWorkToNextHandler_WhenNextHandlerIsNotNull()
        {
            _sut.SetNext(_calculatorsHandlerMock.Object);

            _sut.Handle(_takeHomeSummeryMock.Object);

            _calculatorsHandlerMock.Verify(x => x.Handle(It.IsAny<ITakeHomeSummaryComposite>()), Times.Once());
        }
        [Fact]
        public void ShouldNotHandleWorkToNextHandler_WhenNextHandlerIsNull()
        {
            _sut.Handle(_takeHomeSummeryMock.Object);

            _calculatorsHandlerMock.Verify(x => x.Handle(It.IsAny<ITakeHomeSummaryComposite>()), Times.Never());
        }

    }
}
