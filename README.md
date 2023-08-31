# UKTakeHomeCalculator


UKTakeHomeCalc is a C# library designed to simplify the task of calculating take home salary amounts for employees in the UK. This library is built with the latest rules in mind, ensuring accurate calculations every time. With just a few lines of code, this library can calculate gross pay, various types of deductions, and take-home pay. Designed to be easy to integrate, customize and extend, this library can help with take-home pay calculations.


## **Getting Started:**

This library has no requirement. 

This library can be installed as a NuGet package or manually downloaded from the release section and added to projects.

- #### Using NuGet package manager:

  You can install the package from nuget by searching '**HaitemBahri.UKTakeHomeCalc**' on the NuGet Package Manager or by running the following command on the CLI:
  ```
  dotnet add package HaitemBahri.UKTakeHomeCalc --version 0.1.0
  ```

  Or using the Package Manager Console:
  ```
  NuGet\Install-Package HaitemBahri.UKTakeHomeCalc -Version 0.1.0
  ```

- #### Installing manually:

  Download the built dll file from [Releases](https://github.com/HaitemBahri//UK-Take-Home-Calc/releases) and manually add it to your project.

## **Overview:**

The library relies on two classes to perform the calculations:
1. **CalculatorsHandler**: 
Represent a single calculation stage, where each stage executes multiple `ICalculator` calculations simultineously. This class follows the *Chain of Responsibility pattern*. It's meant to execute multiple calculations in certain order. Each calculation is represented by an `ICalculator` object. The order of calculations matters. For example, pension is calculated before tax, while student loans are calculated after.
It takes an instance of one or more `ICalculator` objects. `ICalculator` objects in a single `ICalculatorsHandle` class are executed in parallel, i.e. calculated using the same income value (from `TakeHomeSummaryComposite` total value).

2. **Calculator**: 
This class represnts a single calculation process. It takes a string that represent the name of the calculation and one or more `ICalculationStrategy` objects. Example: a `Calculator` objects that is responsible for tax calculation in scotland would have a “Tax” as name and ScotlandTaxStrategy as its `ICalculationStrategy`.

3. **ICalculationStrategy**: 
This follows the *Strategy pattern* and It holds that rules of how a calculation should be carried out. It takes a string that represents the name of the strategy and a `MonetaryValue` that represents the free allowance value. Example: `EnglandTaxStrategy` holds the rules of tax calculation for england, it would have a “Tax / England” or “Tax (England)” as a name and 12,570 as that free allownace value.

4. **TakeHomeSummaryItem / TakeHomeSummaryComposite**: 
These classes represent the summary output that lists all income and deductables. They follow the *Composite Pattern* where `ITakeHomeSummaryItem` represent a leaf and `ITakeHomeSummaryComposite` represents a node. 

    When executing `takeHomeSummaryComposite.ToString( )` the **output** looks similar to this:

```
Take-Home Summary
	    Income
                Fixed Income =  1,054.79/Weekly          4,520.55/Monthly        55,000.00/Annually
                Overtime Income =       17.84/Weekly     76.44/Monthly   930.00/Annually
        Pension
                Variable Rate Pension
                        @ [%-5.00] =    -42.22/Weekly    -180.95/Monthly         -2,201.50/Annually
        Tax
                Tax (Scotland)
                        @ [%-19.00] =   -7.88/Weekly     -33.76/Monthly          -410.78/Annually
                        @ [%-20.00] =   -42.02/Weekly    -180.10/Monthly         -2,191.20/Annually
                        @ [%-21.00] =   -72.39/Weekly    -310.24/Monthly         -3,774.54/Annually
                        @ [%-42.00] =   -81.08/Weekly    -347.50/Monthly         -4,227.93/Annually
                        @ [%-47.00] =   0.00/Weekly      0.00/Monthly    0.00/Annually
        National Insurance
                Class 1
                        @ [%-12.00] =   -87.00/Weekly    -372.86/Monthly         -4,536.43/Annually
                        @ [%-2.00] =    -1.27/Weekly     -5.44/Monthly   -66.13/Annually

```


## **Examples:**

### Example 1:
- **Income**:
  - Fixed Income: 55,000/Annually
  - Overtime: 15.5 an hour for 20 hours/monthly for 3 months/Annually
- **Pension**:
  - Auto-enrolment (5%)
- **Tax**:
  - Scotland Tax Rates - Standard free allowance
- **National Insurance**:
  - Class 1 National Insurance contributions

The code will be as follows:
```csharp
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
Console.WriteLine();
```

### Example 2:

- **Income**:
  - Hourly Rate Income:  40 hours/Weekly @ (15.5 an hour)
  - Overtime: 4 hours/Weekly @ (17.5 an hour)
  - Bonus: 150/Monthly
- **Tax**:
  - England Tax Rates - Blind person free allownace
- **National Insurance**:
  - Class 1 National Insurance contributions
- **Student Loans**:
  - Plan 1

The code will be as follows:
```csharp
//Hourly Rate Income: (15.5 an hour)/Weekly
var hourlyRateIncomeStrategy = new HourlyRateIncomeStrategy("Hourly Rate Income", 15.5m, 40m, Frequency.Weekly);

//Overtime: 4 hours/Weekly @ (17.5 an hour)
var overtimeIncomeStrategy = new HourlyRateIncomeStrategy("Overtime Income", 17.5m, 4m, Frequency.Weekly);

//Bonus: 150/Monthly
var bonusIncomeStrategy = new BasicIncomeStrategy("Bonus Income", 150m, Frequency.Monthly);

//Creating a Calculator object to calculate total income
var incomeCalculator = new Calculator("Income", hourlyRateIncomeStrategy, overtimeIncomeStrategy, bonusIncomeStrategy);

//Plan 1 Student Loan
var plan1StudentLoanStrategy = new Plan1StudentLoanStrategy("Plan 1 Student Loan");

//Creating a Calculator object to calculate Student loan
var studentLoanCalculator = new Calculator("Student Loan", plan1StudentLoanStrategy);

//England Tax Rates - Blind person free allownace
var taxStrategy = new EnglandTaxStrategy("Tax (England)", FreeAllowances.Tax.BlindTaxFreeAllowance);

//Creating a Calculator object to calculate tax
var taxCalculator = new Calculator("Tax", taxStrategy);

//Class 1 National Insurance contributions
var nationalInsuranceStrategy = new ClassOneNationalInsuranceStrategy("Class 1", FreeAllowances.NationalInsurance.StandardNationalInsuranceFreeAllowance);

//Creating a Calculator object to calculate national insurance contributions
var nationalInsuranceCalculator = new Calculator("National Insurance", nationalInsuranceStrategy);

//CalculatorsHandler for 1st stage for calculations. Handles Income Calculator
var handler1 = new CalculatorsHandler(incomeCalculator);

//CalculatorsHandler for 2nd stage for calculations, Handles Student Loan, Tax and National Insurance Calculators simultaneously
var handler2 = new CalculatorsHandler(studentLoanCalculator, taxCalculator, nationalInsuranceCalculator);

///Setting the order of the execution.
///handler1 (1st stage) -> handler2 (2nd stage)
handler1.SetNext(handler2);

///Preparing a TakeHomeSummary object that will be passed through the execution stages.
///In each stage, the result from the calculation will be added to it.
var takeHomeSummary = new TakeHomeSummaryComposite("Example 2 Take-Home Summary");

//Starting the Take-Home calculation process.
handler1.Handle(takeHomeSummary);

//Printing detailed result of the Take-Home Summary
Console.WriteLine(takeHomeSummary.ToString());

//Printing the final Value
var takeHomeValue = takeHomeSummary.GetTotal();
Console.WriteLine($"\nTake-Home Value: {takeHomeValue.ConvertTo(Frequency.Weekly)} {takeHomeValue.ConvertTo(Frequency.Monthly)} {takeHomeValue.ConvertTo(Frequency.Annually)}");


```

## **Support:**

In case you have something to discuss, please use the [Discussion](https://github.com/HaitemBahri//UK-Take-Home-Calc/discussions) section. Alternatively, You can contact me directly at haitem_bahri@yahoo.com. All types of feedback are welcome.

## **License**

This project is licensed under the MIT license. See the [LICENSE](LICENSE) file for more info.






