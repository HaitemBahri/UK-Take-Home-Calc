using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculationStrategies.PensionStrategy
{
    public class AutoEnrolmentRatePensionStrategy : ICalculationStrategy
    {
        private MonetaryValue LOWER_THRESHOLD => new MonetaryValue(6_240m, Frequency.Annually);
        private MonetaryValue UPPER_THRESHOLD => new MonetaryValue(50_270m, Frequency.Annually);
        private readonly string _name;
        private readonly float _percentage;

        public AutoEnrolmentRatePensionStrategy(string name, float percentage)
        {
            _name = name;
            _percentage = percentage;
        }
        public ITakeHomeSummaryItem CreateSalaryItem(ITakeHomeSummaryComposite takeHomeSummery)
        {
            var salaryValue = takeHomeSummery.GetTotal();
            var pensionableSalary = GetPensionableSalary(salaryValue);

            var pensionValue = pensionableSalary * (decimal)_percentage * -1;

            return new TakeHomeSummaryItem(_name, pensionValue);
        }

        private MonetaryValue GetPensionableSalary(MonetaryValue salaryValue)
        {
            var pensionableSalary = salaryValue > UPPER_THRESHOLD ? UPPER_THRESHOLD : salaryValue;
            pensionableSalary = pensionableSalary > LOWER_THRESHOLD ? pensionableSalary - LOWER_THRESHOLD : 0m;

            return pensionableSalary;
        }
    }
}
