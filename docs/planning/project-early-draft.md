# "UKTakeHomeCalc" Project early draft

This draft simplifies the logic behind tax calculations in the UK.

## Terminology

* **Gross Income**: Is the total income before any deductions.

* **Untaxable Deductions**: Deductions that are taken from gross income before any Tax/NI deductions.

* **Personal Allowance**: amount of income that can a person get before paying any Tax/NI on.

* **Taxable Income**:

* **Tax**:

* **National Insurance**:

* **Tax-free Income**:

* **Deductions**:

## Steps:
1. Calculate total income > **Gross Income**.

1. Calculate Untaxable Deductions > **Untaxable Deductions**.

1. Calculate tax-free allowance > **Personal Allownace**.

1. Subtract **Personal allowance** and **Untaxable Deductions** from **Gross Income** > **Taxable Income**
    ```
    Gross Income - Untaxable Deductions - Personal Allowance = Taxable Income. 
    ```

1. Calculate tax liability > **Tax Due**.

1. Calculate National Insurance liability > **National Insurance Due**.

1. Subtract tax due from total income > **Tax free income**.
    ```
    Taxable Income - Tax Due - National Insurance Due = Tax Free Income
    ```

1. Calculate Deductables > **Deductables**.

1. Subtract Deductables from Tax Free Income > **Take Home income**.
    ```
    Tax Free Income - Deductables = Take Home Income
    ```

## Parameters Breakdown:

1. Gross Income

    1. Basic Income

    1. Hourly Rate

    1. Overtime

    1. Bonus

    1. Extra Income

1. Untaxable Deductions

    1. Pension

1. Personal Allownace

1. Deductions
    
    1. Student Loans



## References
1. https://www.litrg.org.uk/tax-guides/tax-basics/how-do-i-work-out-my-tax
1. https://www.thesalarycalculator.co.uk/salary.php


