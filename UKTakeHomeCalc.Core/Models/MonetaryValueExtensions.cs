﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKTakeHomeCalc.Core.Models
{
    public static class MonetaryValueExtensions
    {
        public static MonetaryValue Weekly(this decimal value)
        {
            return new MonetaryValue(value, Frequency.Weekly);
        }

        public static MonetaryValue Monthly(this decimal value)
        {
            return new MonetaryValue(value, Frequency.Monthly);
        }

        public static MonetaryValue Annually(this decimal value)
        {
            return new MonetaryValue(value, Frequency.Annually);
        }
    }
}
