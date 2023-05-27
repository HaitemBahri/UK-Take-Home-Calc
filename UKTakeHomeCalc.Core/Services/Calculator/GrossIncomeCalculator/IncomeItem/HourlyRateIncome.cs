using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator.GrossIncomeCalculator.IncomeItem
{
    public class HourlyRateIncome : IIncomeItem
    {
        private readonly MonetaryValue _monetaryValue;
        private readonly string _name;

        public HourlyRateIncome(string name, decimal hourlyRate, decimal hours, Frequency frequency)
        {
            _monetaryValue = new MonetaryValue(hourlyRate * hours, frequency);
            _name = name;
        }
        public ISalaryItem CreateSalaryItem()
        {
            return new SalaryItem(_name, _monetaryValue);
        }
    }
}
