using System.Text.RegularExpressions;

namespace UKTakeHomeCalc.Core.Models
{
    public abstract class SalaryItem
    {
        public string Name { get; }
        public MonetaryValue Value { get; }
        public SalaryItem(string name, MonetaryValue value)
        {
            Name = name;
            Value = value;
        }

        public abstract SalaryItem FindValue(string name);
        public abstract MonetaryValue GetTotal();

    }


    public class SalaryItemLeaf : SalaryItem
    {
        
        public SalaryItemLeaf(string name, MonetaryValue value)
            : base(name, value)
        {

        }
         
        public override MonetaryValue GetTotal()
        {
            throw new NotImplementedException();
        }

        public override SalaryItem FindValue(string name)
        {
            throw new NotImplementedException();
        }
    }

    public class SalaryItemComposite : SalaryItem
    {
        private List<SalaryItem> _salaryItems;
        public SalaryItemComposite(string name) : base(name, new MonetaryValue(0, Frequency.WEEKLY))
        {
            _salaryItems = new List<SalaryItem>();
        }

        public void AddValue(MonetaryValue value)
        {
            throw new NotImplementedException();
        }

        public override SalaryItem FindValue(string name)
        {
            throw new NotImplementedException();
        }

        public override MonetaryValue GetTotal()
        {
           throw new NotImplementedException();
        }
    }

}