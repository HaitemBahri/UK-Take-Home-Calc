namespace UKTakeHomeCalc.Core.Models
{
    public interface ISalaryItemComposite : ISalaryItem
    {
        public void AddValue(ISalaryItem value);
    }
}