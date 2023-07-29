using System.Text;
using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TakeHomeSummaryItems
{
    public class TakeHomeSummaryComposite : ITakeHomeSummaryComposite
    {
        private readonly List<ITakeHomeSummaryItem> _items;
        public string Name { get; }

        public TakeHomeSummaryComposite(string name)
        {
            Name = name;
            _items = new List<ITakeHomeSummaryItem>();
        }

        public void AddValue(ITakeHomeSummaryItem value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _items.Add(value);
        }

        public ITakeHomeSummaryItem? FindValue(string name)
        {
            if (Name == name)
            {
                return this;
            }

            foreach (ITakeHomeSummaryItem value in _items)
            {
                var item = value.FindValue(name);
                if (item != null)
                    return item;
            }
            return null;
        }

        public MonetaryValue GetTotal()
        {
            var total = new MonetaryValue();

            foreach (var item in _items)
            {
                total += item.GetTotal();
            }

            return total;
        }

        public IEnumerable<ITakeHomeSummaryItem> GetSubItems()
        {
            return _items.ToList();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (obj.GetType() != typeof(TakeHomeSummaryComposite))
                return false;

            var other = (ITakeHomeSummaryComposite)obj;

            var result = true;

            result = result && (Name == other.Name);

            result = result && _items.SequenceEqual(other.GetSubItems());

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

            stringBuilder.Append(Name);

            foreach (var item in _items)
            {
                stringBuilder.Append('\n');

                var itemString = item.ToString();

                var updatedItemString = itemString?.Replace("\n", "\n\t");

                stringBuilder.Append('\t').Append(updatedItemString);
            }

            return stringBuilder.ToString();
        }
    }
}