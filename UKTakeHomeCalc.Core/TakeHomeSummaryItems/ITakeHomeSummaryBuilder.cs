using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TakeHomeSummaryItems
{
    public interface ITakeHomeSummaryBuilder
    {
        public ITakeHomeSummaryBuilder Add(string itemName, MonetaryValue itemValue);
        public ITakeHomeSummaryBuilder Add(params ITakeHomeSummaryComposite[] composite);
        public ITakeHomeSummaryComposite Build();
    }
}
