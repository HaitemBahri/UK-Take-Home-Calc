using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.FreeAllowances
{
    public class BlindTaxFreeAllowance : IFreeAllowance
    {
        private readonly MonetaryValue _standardTaxFreeAllowance = 15440m.Annually();
        public MonetaryValue GetFreeAllowance()
        {
            return _standardTaxFreeAllowance;
        }
    }
}
