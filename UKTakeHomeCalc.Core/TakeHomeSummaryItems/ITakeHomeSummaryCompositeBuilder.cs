﻿using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TieredValueCalculators;

namespace UKTakeHomeCalc.Core.TakeHomeSummaryItems
{
    public interface ITakeHomeSummaryCompositeBuilder
    {
        public ITakeHomeSummaryCompositeBuilder Add(string itemName, MonetaryValue itemValue);
        public ITakeHomeSummaryCompositeBuilder Add(params ITakeHomeSummaryItem[] composite);
        public ITakeHomeSummaryCompositeBuilder Add(params TieredValueResult[] results);
        public ITakeHomeSummaryComposite Build();
    }
}
