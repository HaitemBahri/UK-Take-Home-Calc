namespace UKTakeHomeCalc.Core.Models
{
    public class SalaryItem : ISalaryItem
    {
        public MonetaryValue Value { get; }
        public string Name { get; }
        public SalaryItem(string name, MonetaryValue value)
        {
            Name = name;
            Value = value;
        }
        public MonetaryValue GetTotal()
        {
            return Value;
        }
        public ISalaryItem? FindValue(string name)
        {
            if (name == Name)
                return this;

            return null;
        }
    }
}