using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Core.Pension;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Core.Calculator
{
    public class PensionCalculator : ICalculator
    {
        private List<IPensionStrategy> _pensionStrategies;

        public PensionCalculator(List<IPensionStrategy> pensionStrategies)
        {
            _pensionStrategies = pensionStrategies;
        }
        public MonetaryValue Calculate(MonetaryValue value)
        {
            throw new NotImplementedException();
        }
    }
}
