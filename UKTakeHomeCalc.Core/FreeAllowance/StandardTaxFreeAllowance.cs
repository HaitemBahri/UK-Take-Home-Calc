using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.FreeAllowance
{
    public class StandardTaxFreeAllowance : IFreeAllowance
    {
        private readonly MonetaryValue _StandardTaxFreeAllowance = 12570m.Annually();
        public MonetaryValue GetFreeAllowance()
        {
            return _StandardTaxFreeAllowance;
        }
    }
}
