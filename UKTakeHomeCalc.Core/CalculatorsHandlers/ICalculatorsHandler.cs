using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.CalculatorsHandlers
{
    public interface ICalculatorsHandler
    {
        public void SetNext(ICalculatorsHandler calculatorsHandler);
        public void Handle(ISalaryItemNode salaryData);

    }
}
