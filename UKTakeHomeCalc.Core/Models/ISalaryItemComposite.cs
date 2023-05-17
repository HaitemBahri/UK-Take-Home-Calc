namespace UKTakeHomeCalc.Core.Models
{
    public interface ISalaryItemNode : ISalaryItem
    {
        public void AddValue(ISalaryItem value);
    }
}