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

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            var value = ((SalaryItem)obj).Value;
            var name = ((SalaryItem)obj).Name;

            if (Value == value && Name == name)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int initialHash = 23;

            unchecked
            {
                initialHash = initialHash * 23 + Value.GetHashCode();
                initialHash = initialHash * 23 + Name.GetHashCode();
            }

            return initialHash;
        }

        public override string ToString()
        {
            return $"{Name} = {Value}";
        }
    }
}