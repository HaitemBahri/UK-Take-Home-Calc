namespace UKTakeHomeCalc.Core.Models
{
    public class SalaryItem : SalaryItemBase
    {
        public MonetaryValue Value { get; }

        public SalaryItem(string name, MonetaryValue value)
            : base(name)
        {
            Value = value;
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

}