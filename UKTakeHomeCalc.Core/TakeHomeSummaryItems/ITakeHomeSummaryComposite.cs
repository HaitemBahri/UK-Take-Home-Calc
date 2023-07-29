namespace UKTakeHomeCalc.Core.TakeHomeSummaryItems
{
    public interface ITakeHomeSummaryComposite : ITakeHomeSummaryItem
    {
        public void AddValue(ITakeHomeSummaryItem value);
        public IEnumerable<ITakeHomeSummaryItem> GetSubItems();
    }
}