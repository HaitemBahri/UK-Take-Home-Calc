using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem
{
    public class BasicIncome : IIncomeItem
    {
        private readonly MonetaryValue _monetaryValue;
        private readonly string _name;

        public BasicIncome(string name, decimal value, Frequency frequency)
        {
            _monetaryValue = new MonetaryValue(value, frequency);
            _name = name;
        }
        public ISalaryItem CreateSalaryItem()
        {
            return new SalaryItem(_name, _monetaryValue);
        }
    }
}
