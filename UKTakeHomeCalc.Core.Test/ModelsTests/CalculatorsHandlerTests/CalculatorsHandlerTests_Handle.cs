using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator;
using UKTakeHomeCalc.Core.Services.CalculatorsHandler;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.CalculatorsHandlerTests
{
    public class CalculatorsHandlerTests_Handle
    {
        private Mock<ICalculator> _calculatorMock1;
        private Mock<ICalculator> _calculatorMock2;
        private Mock<ICalculator> _calculatorMock3;

        private Mock<ICalculatorsHandler> _calculatorsHandlerMock;

        private Mock<ISalaryItemNode> _nodeMock;
        private List<ICalculator> _calculatorsList;

        public CalculatorsHandlerTests_Handle()
        {
            _calculatorMock1 = new Mock<ICalculator>();
            _calculatorMock2 = new Mock<ICalculator>();
            _calculatorMock3 = new Mock<ICalculator>();

            _calculatorsHandlerMock = new Mock<ICalculatorsHandler>();

            _nodeMock = new Mock<ISalaryItemNode>();
            
            _calculatorMock1.Setup(x => x.CreateSalaryItemNode(null!)).Returns(new SalaryItemNode("some node 1"));
            _calculatorMock2.Setup(x => x.CreateSalaryItemNode(null!)).Returns(new SalaryItemNode("some node 2"));
            _calculatorMock3.Setup(x => x.CreateSalaryItemNode(null!)).Returns(new SalaryItemNode("some node 3"));

            _calculatorsList = new List<ICalculator>() { 
                _calculatorMock1.Object,
                _calculatorMock2.Object,
                _calculatorMock3.Object
            };
        }

        [Fact]
        public void ShouldAddSalaryItemsToSalaryData()
        {
            var sut = new CalculatorsHandler(_calculatorsList);

            sut.Handle(_nodeMock.Object);

            _nodeMock.Verify(x => x.AddValue(It.IsAny<ISalaryItem>()), Times.Exactly(_calculatorsList.Count));
        }
        [Fact]
        public void ShouldHandleWorkToNextHandler_WhenNextHandlerIsNotNull()
        {
            var sut = new CalculatorsHandler(_calculatorsList);

            sut.SetNext(_calculatorsHandlerMock.Object);

            sut.Handle(_nodeMock.Object);

            _calculatorsHandlerMock.Verify(x => x.Handle(It.IsAny<ISalaryItemNode>()), Times.Once());

        }
        [Fact]
        public void ShouldNotHandleWorkToNextHandler_WhenNextHandlerIsNull()
        {
            var sut = new CalculatorsHandler(_calculatorsList);

            sut.Handle(_nodeMock.Object);

            _calculatorsHandlerMock.Verify(x => x.Handle(It.IsAny<ISalaryItemNode>()), Times.Never());

        }

    }
}
