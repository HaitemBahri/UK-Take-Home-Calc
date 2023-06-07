using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxableSalaryCalculationService;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxStrategy;
using Xunit;
using UKTakeHomeCalc.Core.Helpers;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.TaxCalculatorTests
{
    public class TaxCalculatorTests
    {
        private TaxCalculator _sut;
        private Mock<ITaxStrategy> _taxStrategyMock;
        private Mock<ISalaryItemNode> _nameNodeMock;
        private Mock<ISalaryItemNode> _salaryMock;

        public TaxCalculatorTests()
        {
            _taxStrategyMock = new Mock<ITaxStrategy>();
            _nameNodeMock = new Mock<ISalaryItemNode>();
            _salaryMock = new Mock<ISalaryItemNode>();
            _sut = new TaxCalculator(_nameNodeMock.Object, _taxStrategyMock.Object);
            
            _salaryMock.Setup(x => x.GetTotal()).Returns(4599m.Annually());
            _taxStrategyMock.Setup(x => x.CreateTaxSalaryItem(It.IsAny<ISalaryItem>()))
                            .Returns(new SalaryItem("Some salaryItem Name", 56993m.Annually()));
        }

        [Fact]
        public void CreateSalaryItemNode_ShouldGetTaxSalaryItemFromTaxStrategy()
        {
            _sut.CreateSalaryItemNode(_salaryMock.Object);

            _taxStrategyMock.Verify(x => x.CreateTaxSalaryItem(It.IsNotNull<ISalaryItem>()), Times.Once);
        }
        [Fact]
        public void CreateSalaryItemNode_ShouldAddTaxSalaryItemToItsNameNode()
        {
            _sut.CreateSalaryItemNode(_salaryMock.Object);

            _nameNodeMock.Verify(x => x.AddValue(It.IsNotNull<ISalaryItem>()), Times.Once);
        }
        [Fact]
        public void CreateSalaryItemNode_ShouldReturnItsNameNode()
        {
            var expectResult = _nameNodeMock.Object;

            var actualResult = _sut.CreateSalaryItemNode(_salaryMock.Object);

            Assert.Equal(expectResult, actualResult);
        }

    }
}
