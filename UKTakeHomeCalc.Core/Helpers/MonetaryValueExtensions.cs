using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Helpers
{
    public static class MonetaryValueExtensions
    {
        public static MonetaryValue Weekly(this decimal value)
        {
            return new MonetaryValue(value, Frequency.WEEKLY);
        }

        public static MonetaryValue Monthly(this decimal value)
        {
            return new MonetaryValue(value, Frequency.MONTHLY);
        }

        public static MonetaryValue Annually(this decimal value)
        {
            return new MonetaryValue(value, Frequency.ANNUALLY);
        }
    }
}
