```mermaid
classDiagram
    class ThresholdCalculationService{
        CalculateValueBetweenThresholds(value, lowerThreshold, upperThreshold): MonetaryValue

        CalculateThresholdPercentageResult(value, List~ThresholdPercentageRule~) : List~PercentageCalculationResult~
    }

    class ThresholdPercentageRule{
        FromValue: MonetaryValue
        ToValue: MonetaryValue
        Percentage: decimal
    }

    class ThresholdPercentageResult{
        Rule: ThresholdPercentageRule
        Result: MonetaryValue
    }


