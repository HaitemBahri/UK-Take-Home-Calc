﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.FreeAllowances.TaxFreeAllowance
{
    public interface ITaxFreeAllowance
    {
        public MonetaryValue GetTaxFreeAllowance();
    }
}