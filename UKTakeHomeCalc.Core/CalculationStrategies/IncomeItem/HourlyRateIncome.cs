using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.CalculationStrategies.IncomeItem
{
    public class HourlyRateIncome : ICalculationStrategy
    {
        private readonly MonetaryValue _monetaryValue;
        private readonly string _name;

        public HourlyRateIncome(string name, decimal hourlyRate, decimal hours, Frequency frequency)
        {
            _monetaryValue = new MonetaryValue(hourlyRate * hours, frequency);
            _name = name;
        }
        public ISalaryItem CreateSalaryItem(ISalaryItemNode takeHomeSummery)
        {
            return new SalaryItem(_name, _monetaryValue);
        }
    }
}
