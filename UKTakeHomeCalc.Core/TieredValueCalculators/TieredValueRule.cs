﻿using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public struct TieredValueRule
    {
        public MonetaryValue FromValue { get; }
        public MonetaryValue ToValue { get; }
        public decimal Percentage { get; }

        public TieredValueRule(MonetaryValue fromValue, MonetaryValue toValue, decimal percentage)
        {
            FromValue = fromValue;
            ToValue = toValue;
            Percentage = percentage; 
        }

        public override string ToString()
        {
            return $"[From {FromValue} - To {ToValue}] %{Percentage*100:N2}";
        }
    }
}
