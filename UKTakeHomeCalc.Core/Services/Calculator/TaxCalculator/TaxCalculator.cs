using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxStrategy;

namespace UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator
{
    internal class TaxCalculator : ICalculator
    {
        private ITaxStrategy _taxStrategy;
        private ISalaryItemNode _nameNode;

        public TaxCalculator(ISalaryItemNode nameNode, ITaxStrategy taxStrategy)
        {
            _taxStrategy = taxStrategy;
            _nameNode = nameNode;
        }

        public ISalaryItemNode CreateSalaryItemNode(ISalaryItemNode salary)
        {
            _nameNode.AddValue(_taxStrategy.CreateTaxSalaryItem(salary));

            return _nameNode;
        }
    }
}
