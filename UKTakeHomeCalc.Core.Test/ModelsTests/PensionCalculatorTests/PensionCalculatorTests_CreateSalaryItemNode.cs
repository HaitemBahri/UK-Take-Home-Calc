using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator;
using Xunit;
using UKTakeHomeCalc.Core.Services.Calculator.PensionCalculator.PensionStrategy;
using UKTakeHomeCalc.Core.Services.Calculator.PensionCalculator;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.PensionCalculatorTests
{
    public class PensionCalculatorTests_CreateSalaryItemNode
    {
        private Mock<IPensionStrategy> _pensionMock1;
        private Mock<IPensionStrategy> _pensionMock2;
        private Mock<IPensionStrategy> _pensionMock3;
        private Mock<ISalaryItemNode> _nameNodeMock;
        private Mock<ISalaryItemNode> _salaryMock;

        public PensionCalculatorTests_CreateSalaryItemNode()
        {
            _pensionMock1 = new Mock<IPensionStrategy>();
            _pensionMock2 = new Mock<IPensionStrategy>();
            _pensionMock3 = new Mock<IPensionStrategy>();
            _nameNodeMock = new Mock<ISalaryItemNode>();
            _salaryMock = new Mock<ISalaryItemNode>();
        }

        [Fact]
        public void ShouldGetValuesFromIPensionStrategys()
        {
            _pensionMock1.Setup(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>())).Returns(new SalaryItem("test", 0.0m));
            _pensionMock2.Setup(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>())).Returns(new SalaryItem("test", 0.0m));
            _pensionMock3.Setup(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>())).Returns(new SalaryItem("test", 0.0m));


            var sut = new PensionCalculator(_nameNodeMock.Object, new List<IPensionStrategy>()
            {
                _pensionMock1.Object,
                _pensionMock2.Object,
                _pensionMock3.Object,

            });

            var x = sut.CreateSalaryItemNode(_salaryMock.Object);

            _pensionMock1.Verify(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>()), Times.Exactly(1));
            _pensionMock2.Verify(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>()), Times.Exactly(1));
            _pensionMock3.Verify(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>()), Times.Exactly(1));
        }
        [Fact]
        public void ShouldAddSalaryItemsFromIncomeItemsToItsNameNode()
        {
            _pensionMock1.Setup(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>())).Returns(new SalaryItem("test", 0.0m));
            _pensionMock2.Setup(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>())).Returns(new SalaryItem("test", 0.0m));
            _pensionMock3.Setup(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>())).Returns(new SalaryItem("test", 0.0m));

            var sut = new PensionCalculator(_nameNodeMock.Object, new List<IPensionStrategy>()
            {
                _pensionMock1.Object,
                _pensionMock2.Object,
                _pensionMock3.Object,

            });

            var x = sut.CreateSalaryItemNode(_salaryMock.Object);

            _nameNodeMock.Verify(x => x.AddValue(It.IsAny<ISalaryItem>()), Times.Exactly(3));
        }
        [Fact]
        public void ShouldReturnItsNameNodeWithSalaryItems()
        {
            var salaryItemNode = new SalaryItemNode("myNode name");
            _pensionMock1.Setup(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>())).Returns(new SalaryItem("test", 0.0m));
            _pensionMock2.Setup(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>())).Returns(new SalaryItem("test", 0.0m));
            _pensionMock3.Setup(x => x.CreatePensionSalaryItem(It.IsAny<ISalaryItemNode>())).Returns(new SalaryItem("test", 0.0m));

            var sut = new PensionCalculator(salaryItemNode, new List<IPensionStrategy>()
            {
                _pensionMock1.Object,
                _pensionMock2.Object,
                _pensionMock3.Object,

            });

            var actualValue = sut.CreateSalaryItemNode(_salaryMock.Object);

            Assert.Equal(salaryItemNode, actualValue);
            Assert.Equal(3, actualValue.GetSalaryItems().Count());
        }
    }
}
