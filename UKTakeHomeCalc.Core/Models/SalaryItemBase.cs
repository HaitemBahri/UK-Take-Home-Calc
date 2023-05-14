using System.Text.RegularExpressions;

namespace UKTakeHomeCalc.Core.Models
{
    public abstract class SalaryItemBase
    {
        public string Name { get; }
        public SalaryItemBase(string name)
        {
            Name = name;
        }

        public abstract SalaryItemBase? FindValue(string name);
        public abstract MonetaryValue GetTotal();
    }

}