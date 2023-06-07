using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxStrategy;

namespace UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator
{
    public class TaxCalculator : ICalculator
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
            var taxSalaryItem = _taxStrategy.CreateTaxSalaryItem(salary);
            _nameNode.AddValue(taxSalaryItem);

            return _nameNode;
        }
    }
}
