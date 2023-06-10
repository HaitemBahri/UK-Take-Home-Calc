﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.CalculationStrategies;
using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.Models.CalculationRules;
using UKTakeHomeCalc.Core.QualifyingSalaryServices.QualifyingSalaryCalculationService;

namespace UKTakeHomeCalc.Core.CalculationStrategies.NationalInsuranceStrategy
{
    public class Class1NationalInsuranceStrategy : ICalculationStrategy
    {
        private readonly string _name;
        private readonly CalculationRule _nationalInsuranceCalculationRule = new CalculationRule();

        public Class1NationalInsuranceStrategy(string name, IQualifyingSalaryCalculationService qualifyingSalaryCalculationService)
        {
            _name = name;
            _nationalInsuranceCalculationRule = new CalculationRule();

            _nationalInsuranceCalculationRule.AddRule(0m.Weekly(), (967m - 242m).Annually(), 0.12m);
            _nationalInsuranceCalculationRule.AddRule((967m - 242m).Annually(), 10000000m.Annually(), 0.02m);
        }

        public ISalaryItem CreateSalaryItem(ISalaryItemNode takeHomeSummery)
        {
            throw new NotImplementedException();

            //TODO: Calculate qualifying salary

            //TODO: Calculate NI contributions

            //TODO: COnstruct salaryItemNode

            //TODO: Return salaryItemNode

        }
    }
}
