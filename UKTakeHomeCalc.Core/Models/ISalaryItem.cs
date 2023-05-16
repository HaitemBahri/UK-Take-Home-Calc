using System.Text.RegularExpressions;

namespace UKTakeHomeCalc.Core.Models
{
    public interface ISalaryItem
    {
        string Name { get; }
        ISalaryItem? FindValue(string name);
        MonetaryValue GetTotal();
    }
}