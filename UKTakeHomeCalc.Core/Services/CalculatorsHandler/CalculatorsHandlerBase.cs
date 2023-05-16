using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.CalculatorsHandler
{
    public abstract class CalculatorsHandlerBase : ICalculatorsHandler
    {
        protected ICalculatorsHandler? _nextCalculatorsHandler = null;

        public abstract void Calculate(MonetaryValue value);

        public void SetNext(ICalculatorsHandler nextCalculatorsHandler)
        {
            _nextCalculatorsHandler = nextCalculatorsHandler;
        }
    }
}
