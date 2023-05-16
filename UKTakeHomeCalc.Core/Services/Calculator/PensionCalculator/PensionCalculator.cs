using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Core.Pension;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Services.Calculator;

namespace UKTakeHomeCalc.Core.Services.Calculator.PensionCalculator
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
