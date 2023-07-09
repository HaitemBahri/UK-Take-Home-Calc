using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.FreeAllowances
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
