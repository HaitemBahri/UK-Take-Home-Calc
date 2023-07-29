using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TakeHomeSummaryItems
{
    public interface ITakeHomeSummaryItem
    {
        string Name { get; }
        ITakeHomeSummaryItem? FindValue(string name);
        MonetaryValue GetTotal();
    }
}