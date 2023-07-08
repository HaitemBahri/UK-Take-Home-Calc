using System.Data.Common;
using System.Text;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TakeHomeSummaryItems
{
    public class TakeHomeSummaryComposite : ITakeHomeSummaryComposite
    {
        private List<ITakeHomeSummaryItem> _salaryItems;
        public TakeHomeSummaryComposite(string name)
        {
            Name = name;
            _salaryItems = new List<ITakeHomeSummaryItem>();
        }
        public string Name { get; }

        public void AddValue(ITakeHomeSummaryItem value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _salaryItems.Add(value);
        }
        public ITakeHomeSummaryItem? FindValue(string name)
        {
            if (Name == name)
            {
                return this;
            }

            foreach (ITakeHomeSummaryItem value in _salaryItems)
            {
                var item = value.FindValue(name);
                if (item != null)
                    return item;
            }
            return null;
        }
        public MonetaryValue GetTotal()
        {
            var total = new MonetaryValue(0, Frequency.Weekly);

            foreach (var item in _salaryItems)
            {
                total += item.GetTotal();
            }

            return total;
        }

        public IEnumerable<ITakeHomeSummaryItem> GetSalaryItems()
        {
            return _salaryItems.ToList();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (obj.GetType() != typeof(TakeHomeSummaryComposite))
                return false;

            var other = (ITakeHomeSummaryComposite)obj;

            var result = true;

            if (Name == other.Name)
                result = result && true;

            result = result && _salaryItems.SequenceEqual(other.GetSalaryItems());

            return result;
        }

        public static bool operator ==(TakeHomeSummaryComposite value1, TakeHomeSummaryComposite value2)
        {
            return (value1.Equals(value2));
        }

        public static bool operator !=(TakeHomeSummaryComposite value1, TakeHomeSummaryComposite value2)
        {
            return !(value1.Equals(value2));
        }

        public override int GetHashCode()
        {
            int initialHash = 23;

            unchecked
            {
                initialHash = initialHash * 23 + Name.GetHashCode();
            }

            return initialHash;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(Name);

            foreach (var item in _salaryItems)
            {
                stringBuilder.Append('\t').AppendLine(item.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}