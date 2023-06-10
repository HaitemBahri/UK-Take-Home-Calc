using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.SalaryItemCompositeTests
{
    public class SalaryItemNodeTests_ValueComparison
    {
        [Fact]
        public void ShouldTwoSalaryItemNodesBeEqual_WhenNameAndSalaryItemsAreSame()
        {
            var name = "SalaryItemNode Name";
            var sut = new SalaryItemNode(name);
            sut.AddValue(new SalaryItem("SalaryItem 1", 45.2m.Weekly()));
            sut.AddValue(new SalaryItem("SalaryItem 2", 435.2m.Monthly()));
            sut.AddValue(new SalaryItem("SalaryItem 3", 15555.2m.Annually()));

            var sut2 = new SalaryItemNode(name);
            sut2.AddValue(new SalaryItem("SalaryItem 1", 45.2m.Weekly()));
            sut2.AddValue(new SalaryItem("SalaryItem 2", 435.2m.Monthly()));
            sut2.AddValue(new SalaryItem("SalaryItem 3", 15555.2m.Annually()));

            Assert.Equal(sut, sut2);
        }

        [Fact]
        public void ShouldTwoSalaryItemNodesBeNotEqual_WhenNameIsSameButSalaryItemsAreDifferent()
        {
            var name = "SalaryItemNode Name";
            var sut = new SalaryItemNode(name);
            sut.AddValue(new SalaryItem("SalaryItem 1", 45.2m.Weekly()));
            sut.AddValue(new SalaryItem("SalaryItem 3", 15555.2m.Annually()));
            sut.AddValue(new SalaryItem("SalaryItem 2", 435.2m.Monthly()));

            var sut2 = new SalaryItemNode(name);
            sut2.AddValue(new SalaryItem("SalaryItem 1", 45.2m.Weekly()));
            sut2.AddValue(new SalaryItem("SalaryItem 2", 435.2m.Monthly()));
            sut2.AddValue(new SalaryItem("SalaryItem 3", 15555.2m.Annually()));

            Assert.NotEqual(sut, sut2);
        }

        [Fact]
        public void ShouldTwoSalaryItemNodesBeNotEqual_WhenNameIsDifferentButSalaryItemsAreSame()
        {
            var name = "SalaryItemNode Name";
            var sut = new SalaryItemNode(name);
            sut.AddValue(new SalaryItem("SalaryItem 1", 45.2m.Weekly()));
            sut.AddValue(new SalaryItem("SalaryItem 2", 435.2m.Monthly()));
            sut.AddValue(new SalaryItem("SalaryItem 3", 15555.2m.Annually()));

            var name2 = "SalaryItemNode Name - 2";
            var sut2 = new SalaryItemNode(name2);
            sut2.AddValue(new SalaryItem("SalaryItem 1", 45.2m.Weekly()));
            sut2.AddValue(new SalaryItem("SalaryItem 2", 435.2m.Monthly()));
            sut2.AddValue(new SalaryItem("SalaryItem 3", 15555.2m.Annually()));

            Assert.Equal(sut, sut2);
        }

        [Fact]
        public void ShouldTwoSalaryItemNodesBeNotEqual_WhenNameAndSalaryItemsAreDifferent()
        {
            var name = "SalaryItemNode Name";
            var sut = new SalaryItemNode(name);
            sut.AddValue(new SalaryItem("SalaryItem 1", 45.2m.Weekly()));
            sut.AddValue(new SalaryItem("SalaryItem 3", 15555.2m.Annually()));

            var name2 = "SalaryItemNode Name - 2";
            var sut2 = new SalaryItemNode(name2);
            sut2.AddValue(new SalaryItem("SalaryItem 1", 45.2m.Weekly()));
            sut2.AddValue(new SalaryItem("SalaryItem 2", 435.2m.Monthly()));
            sut2.AddValue(new SalaryItem("SalaryItem 3", 15555.2m.Annually()));

            Assert.NotEqual(sut, sut2);
        }
    }
}
