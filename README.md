# UKTakeHomeCalculator
        //TODO, badges for nuget, release version

UKTakeHomeCalc is a C# library designed to simplify the task of calculating take home salary amounts for employees in the UK. This library is built with the latest rules in mind, ensuring accurate calculations every time. With just a few lines of code, this library can calculate gross pay, various types of deductions, and take-home pay. Designed to be easy to integrate, customize and extend, this library can help with take-home pay calculations.


## **Getting Started:**

This library has no requirement. 

This library can be installed as a NuGet package or manually downloaded from the release section and added to projects.

- #### Using NuGet package manager:

        //TODO

- #### Installing manually:

        //TODO

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

    When executing `takeHomeSummaryComposite.ToString( )` the output looks similar to this:

```
Take-Home Summary
	Pension
		Auto-Enrol Pension 
			@ [%-5.00] = 3.28/Weekly 	 14.05/Monthly 	 170.94/Annually
		Additional Pension
			@ [%-2.50] = 1.64/Weekly 	 7.02/Monthly 	 85.47/Annually
	Tax
		England Tax
			@ [%-20.00] = 22.30/Weekly 	 95.55/Monthly 	 1,162.52/Annually
			@ [%-40.00] = 0.94/Weekly 	 4.05/Monthly 	 49.28/Annually
	National Insurance
		Class 1 NI
			@ [%-12.00] = 3.24/Weekly 	 13.88/Monthly 	 168.87/Annually
			@ [%-2.00] = 1.22/Weekly 	 5.23/Monthly 	 63.63/Annually
```

## **Examples:**

        //TODO


## **Support:**

In case you have something to discuss, please use the [Discussion](https://github.com/HaitemBahri//UK-Take-Home-Calc/discussions) section. Alternatively, You can contact me directly at haitem_bahri@yahoo.com. All types of feedback are welcome.

## **License**

This project is licensed under the MIT license. See the [LICENSE](LICENSE) file for more info.






