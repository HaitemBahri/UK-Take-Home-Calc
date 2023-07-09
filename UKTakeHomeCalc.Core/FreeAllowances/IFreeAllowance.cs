using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.FreeAllowances
{
    public interface IFreeAllowance
    {
        public MonetaryValue GetFreeAllowance();
    }
}