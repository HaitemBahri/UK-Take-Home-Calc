using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.SalaryItemCompositeTests
{
    public class SalaryItemCompositeTests_Value
    {
        [Fact]
        public void ShouldReturnItsName()
        {
            var expectedName = "SalaryBreakdown Item Some Name";
            var sut = new SalaryItemNode(expectedName);

            var actualName = sut.Name;

            Assert.Equal(expectedName, actualName);
        }
    }
}
