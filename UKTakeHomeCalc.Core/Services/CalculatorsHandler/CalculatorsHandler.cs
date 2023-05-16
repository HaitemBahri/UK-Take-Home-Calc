using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Core.Calculator;
using UKTakeHomeCalc.Core.Core.CalculatorsHandle;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.CalculatorsHandler
{
    public class CalculatorsHandler : CalculatorsHandlerBase
    {
        private List<ICalculator> _calculators;
        public CalculatorsHandler(List<ICalculator> calculators)
        {
            _calculators = calculators;
        }
        public override void Calculate(MonetaryValue value)
        {
            //TODO: implement logic


            //if (_nextCalculatorsHandler == null)
            //    return;
            //else
            //    _nextCalculatorsHandler.Calculate(salaryData);
        }
    }
}
