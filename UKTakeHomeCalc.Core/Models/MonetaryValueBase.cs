using System.Text.RegularExpressions;

namespace UKTakeHomeCalc.Core.Models
{
    public abstract class SalaryItemBase
    {
        public string Name { get; }
        public virtual MonetaryValue Value { get; }
        public SalaryItemBase(string name, MonetaryValue value)
        {
            Name = name;
            Value = value;
        }

        public abstract SalaryItemBase? FindValue(string name);
        public abstract MonetaryValue GetTotal();
    }


    public class SalaryItem : SalaryItemBase
    {
        
        public SalaryItem(string name, MonetaryValue value)
            : base(name, value)
        {

        }
         
        public override MonetaryValue GetTotal()
        {
            return Value;
        }

        public override SalaryItemBase? FindValue(string name)
        {
            if(name == Name)
                return this;

            return null;
        }
    }

    public class SalaryItemComposite : SalaryItemBase
    {
        private List<SalaryItemBase> _salaryItems;
        public override MonetaryValue Value { get; }
        public SalaryItemComposite(string name) : base(name, null!)
        {
            _salaryItems = new List<SalaryItemBase>();
        }

        public void AddValue(SalaryItemBase value)
        {
            _salaryItems.Add(value);
        }

        public override SalaryItemBase FindValue(string name)
        {
            throw new NotImplementedException();
        }

        public override MonetaryValue GetTotal()
        {
            var total = new MonetaryValue(0, Frequency.WEEKLY);

            foreach(var item in _salaryItems)
            {
                total += item.Value;
            }

            return total;
         
        }
    }

}