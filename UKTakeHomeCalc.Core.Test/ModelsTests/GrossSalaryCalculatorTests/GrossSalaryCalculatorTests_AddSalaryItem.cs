using Moq;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator;
using UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.GrossSalaryCalculatorTests
{
    public class GrossSalaryCalculatorTests_AddSalaryItem
    {
        private Mock<IIncomeItem> _incomeItemMock1;
        private Mock<IIncomeItem> _incomeItemMock2;
        private Mock<IIncomeItem> _incomeItemMock3;
        private Mock<ISalaryItemNode> _nameNodeMock;

        public GrossSalaryCalculatorTests_AddSalaryItem()
        {
            _incomeItemMock1 = new Mock<IIncomeItem>();
            _incomeItemMock2 = new Mock<IIncomeItem>();
            _incomeItemMock3 = new Mock<IIncomeItem>();
            _nameNodeMock = new Mock<ISalaryItemNode>();

        }

        [Fact]
        public void ShouldGetValuesFromIIncomeItems()
        {
            _incomeItemMock1.Setup(x => x.GetValue()).Returns(new SalaryItem("test", 0.0m));
            _incomeItemMock2.Setup(x => x.GetValue()).Returns(new SalaryItem("test", 0.0m));
            _incomeItemMock3.Setup(x => x.GetValue()).Returns(new SalaryItem("test", 0.0m));


            var sut = new GrossSalaryCalculator(_nameNodeMock.Object, new List<IIncomeItem>()
            {
                _incomeItemMock1.Object,
                _incomeItemMock2.Object,
                _incomeItemMock3.Object,

            });

            var x = sut.CreateSalaryItemNode();

            _incomeItemMock1.Verify(x => x.GetValue(), Times.Exactly(1));
            _incomeItemMock2.Verify(x => x.GetValue(), Times.Exactly(1));
            _incomeItemMock3.Verify(x => x.GetValue(), Times.Exactly(1));
        }
        [Fact]
        public void ShouldAddSalaryItemsFromIncomeItemsToItsNameNode()
        {
            _incomeItemMock1.Setup(x => x.GetValue()).Returns(new SalaryItem("test", 0.0m));
            _incomeItemMock2.Setup(x => x.GetValue()).Returns(new SalaryItem("test", 0.0m));
            _incomeItemMock3.Setup(x => x.GetValue()).Returns(new SalaryItem("test", 0.0m));

            var sut = new GrossSalaryCalculator(_nameNodeMock.Object, new List<IIncomeItem>()
            {
                _incomeItemMock1.Object,
                _incomeItemMock2.Object,
                _incomeItemMock3.Object,

            });

            var x = sut.CreateSalaryItemNode();

            _nameNodeMock.Verify(x => x.AddValue(It.IsAny<ISalaryItem>()), Times.Exactly(3));
        }
        [Fact]
        public void ShouldReturnItsNameNodeWithSalaryItems()
        {
            var salaryItemNode = new SalaryItemNode("myNode name");
            _incomeItemMock1.Setup(x => x.GetValue()).Returns(new SalaryItem("test", 0.0m));
            _incomeItemMock2.Setup(x => x.GetValue()).Returns(new SalaryItem("test", 0.0m));
            _incomeItemMock3.Setup(x => x.GetValue()).Returns(new SalaryItem("test", 0.0m));

            var sut = new GrossSalaryCalculator(salaryItemNode, new List<IIncomeItem>()
            {
                _incomeItemMock1.Object,
                _incomeItemMock2.Object,
                _incomeItemMock3.Object,

            });

            var actualValue = sut.CreateSalaryItemNode();

            Assert.Equal(salaryItemNode, actualValue);
            Assert.Equal(3, actualValue.GetSalaryItems().Count());
        }
    }
}
