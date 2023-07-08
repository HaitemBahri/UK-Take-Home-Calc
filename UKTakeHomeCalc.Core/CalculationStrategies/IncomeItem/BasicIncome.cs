using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculationStrategies.IncomeItem
{
    public class BasicIncome : ICalculationStrategy
    {
        private readonly MonetaryValue _monetaryValue;
        private readonly string _name;

        public BasicIncome(string name, decimal value, Frequency frequency)
        {
            _monetaryValue = new MonetaryValue(value, frequency);
            _name = name;
        }
        public ITakeHomeSummaryItem CreateSalaryItem(ITakeHomeSummaryComposite takeHomeSummery)
        {
            return new TakeHomeSummaryItem(_name, _monetaryValue);
        }
    }
}
