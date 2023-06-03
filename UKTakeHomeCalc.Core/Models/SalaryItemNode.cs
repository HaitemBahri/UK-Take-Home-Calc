using System.Text;

namespace UKTakeHomeCalc.Core.Models
{
    public class SalaryItemNode : ISalaryItemNode
    {
        private List<ISalaryItem> _salaryItems;
        public SalaryItemNode(string name)
        {
            Name = name;
            _salaryItems = new List<ISalaryItem>();
        }
        public string Name { get; }

        public void AddValue(ISalaryItem value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _salaryItems.Add(value);
        }
        public ISalaryItem? FindValue(string name)
        {
            if (Name == name)
            {
                return this;
            }

            foreach (ISalaryItem value in _salaryItems)
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

        public IEnumerable<ISalaryItem> GetSalaryItems()
        {
            return _salaryItems.ToList();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) 
                return false;

            if (obj.GetType() != typeof(SalaryItemNode)) 
                return false;

            var other = (ISalaryItemNode)obj;

            var result = true;

            if (Name == other.Name)
                result = result && true;

            result = result && _salaryItems.SequenceEqual(other.GetSalaryItems());

            return result;
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

            foreach(var item in _salaryItems)
            {
                stringBuilder.Append('\t').AppendLine(item.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}