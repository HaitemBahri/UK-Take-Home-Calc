namespace UKTakeHomeCalc.Core.Models
{
    public class SalaryItemComposite : ISalaryItemComposite
    {
        private List<ISalaryItem> _salaryItems;
        public SalaryItemComposite(string name)
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
            var total = new MonetaryValue(0, Frequency.WEEKLY);

            foreach (var item in _salaryItems)
            {
                total += item.GetTotal();
            }

            return total;

        }
    }
}