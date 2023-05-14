namespace UKTakeHomeCalc.Core.Models
{
    public class SalaryItemComposite : SalaryItemBase
    {
        private List<SalaryItemBase> _salaryItems;
        public SalaryItemComposite(string name) : base(name)
        {
            _salaryItems = new List<SalaryItemBase>();
        }

        public void AddValue(SalaryItemBase value)
        {
            if(value == null)
                throw new ArgumentNullException(nameof(value));

            _salaryItems.Add(value);
        }

        public override SalaryItemBase? FindValue(string name)
        {
            if(Name == name)
            {
                return this;
            }

            foreach(SalaryItemBase value in _salaryItems)
            {
                var item = value.FindValue(name);
                if(item != null)
                    return item;
            }
            return null;
        }

        public override MonetaryValue GetTotal()
        {
            var total = new MonetaryValue(0, Frequency.WEEKLY);

            foreach(var item in _salaryItems)
            {
                total += item.GetTotal();
            }

            return total;
         
        }
    }

}