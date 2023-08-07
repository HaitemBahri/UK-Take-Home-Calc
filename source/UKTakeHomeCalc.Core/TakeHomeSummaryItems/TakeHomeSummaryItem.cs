using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TakeHomeSummaryItems
{
    public class TakeHomeSummaryItem : ITakeHomeSummaryItem
    {
        public MonetaryValue Value { get; }
        public string Name { get; }

        public TakeHomeSummaryItem(string name, MonetaryValue value)
        {
            Name = name;
            Value = value;
        }

        public MonetaryValue GetTotal()
        {
            return Value;
        }

        public ITakeHomeSummaryItem? FindValue(string name)
        {
            if (name == Name)
                return this;

            return null;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            var value = ((TakeHomeSummaryItem)obj).Value;
            var name = ((TakeHomeSummaryItem)obj).Name;

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

        public string ToStringExtended()
        {
            return $"{Name} = {Value.ConvertTo(Frequency.Weekly)} \t {Value.ConvertTo(Frequency.Monthly)} \t {Value.ConvertTo(Frequency.Annually)}";

        }
    }
}