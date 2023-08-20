

using UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.NationalInsuranceStrategies;
using UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.PensionStrategies;
using UKTakeHomeCalc.Core.CalculationStrategies.DeductableStrategies.TaxStrategies;
using UKTakeHomeCalc.Core.CalculationStrategies.IncomeStrategies;
using UKTakeHomeCalc.Core.Calculators;
using UKTakeHomeCalc.Core.CalculatorsHandlers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Demo1();
        }

        static void Demo1()
        {
            /*  Example 1:
             *  
             *  Income:
             *      -Fixed Income: 55,000/Annually
             *      -Overtime: 15.5 an hour for 20 hours/monthly for 3 months/Annually
             *  
             *  Pension:
             *      -Auto-enrolment (5%)
             *      
             *  Tax:
             *      -Scotland Tax Rates - Standard free allowance
             *      
             *  National Insurance:
             *      -Class 1 National Insurance contributions
             * 
             */

            //Fixed Income: 55,000/Annually
            var fixedIncomeStrategy = new BasicIncomeStrategy("Fixed Income", 55000m, Frequency.Annually);

            //Overtime: 15.5 an hour for 20 hours/monthly for 3 months/Annually
            var overtimeIncomeStrategy = new HourlyRateIncomeStrategy("Overtime Income", 15.5m, 20 * 3, Frequency.Annually);

            //Creating a Calculator object to calculate total income
            var incomeCalculator = new Calculator("Income", fixedIncomeStrategy, overtimeIncomeStrategy);

            //Auto-enrolment (5%) pension
            var pensionStrategy = new VariableRatePensionStrategy("Variable Rate Pension", 0.05m, FreeAllowances.Pension.StandardPensionFreeAllowance);

            //Creating a Calculator object to calculate pension
            var pensionCalculator = new Calculator("Pension", pensionStrategy);

            //Scotland Tax Rates - Standard free allowance
            var taxStrategy = new ScotlandTaxStrategy("Tax (Scotland)", FreeAllowances.Tax.StandardTaxFreeAllowance);

            //Creating a Calculator object to calculate tax
            var taxCalculator = new Calculator("Tax", taxStrategy);

            //Class 1 National Insurance contributions
            var nationalInsuranceStrategy = new ClassOneNationalInsuranceStrategy("Class 1", FreeAllowances.NationalInsurance.StandardNationalInsuranceFreeAllowance);

            //Creating a Calculator object to calculate national insurance contributions
            var nationalInsuranceCalculator = new Calculator("National Insurance", nationalInsuranceStrategy);

            //CalculatorsHandler for 1st stage for calculations. Handles Income Calculator
            var handler1 = new CalculatorsHandler(incomeCalculator);

            //CalculatorsHandler for 2nd stage for calculations, Handles Pension Calculator
            var handler2 = new CalculatorsHandler(pensionCalculator);

            //CalculatorsHandler for 3rd stage for calculations, Handles both Tax and National Insurance Calculators
            var handler3 = new CalculatorsHandler(taxCalculator, nationalInsuranceCalculator);

            ///Setting the order of the execution.
            ///handler1 (1st stage) -> handler2 (2nd stage) -> handler3 (3rd stage)
            handler1.SetNext(handler2);
            handler2.SetNext(handler3);

            ///Preparing a TakeHomeSummary object that will be passed through the execution stages.
            ///In each stage, the result from the calculation will be added to it.
            var takeHomeSummary = new TakeHomeSummaryComposite("Example 1 Take-Home Summary");

            //Starting the Take-Home calculation process.
            handler1.Handle(takeHomeSummary);

            //Printing detailed result of the Take-Home Summary
            Console.WriteLine(takeHomeSummary.ToString());

            //Printing the final Value
            var takeHomeValue = takeHomeSummary.GetTotal();
            Console.WriteLine($"\nTake-Home Value: {takeHomeValue.ConvertTo(Frequency.Weekly)} {takeHomeValue.ConvertTo(Frequency.Monthly)} {takeHomeValue.ConvertTo(Frequency.Annually)}");
        }

        static void Demo2()
        {

            /*  Example 1:
             *  
             *  Income:
             *      -Hourly Rate Income: (15.5 an hour)/Weekly
             *      -Overtime: 4 hours/Weekly
             *      -Bonus: 150/Monthly
             *  
             *  Tax:
             *      -England Tax Rates - Blind person free allownace
             *      
             *  National Insurance:
             *      -Class 1 National Insurance contributions
             *      
             *  Student Loans:
             *      -Plan 1
             * 
             */

        }
    }
}