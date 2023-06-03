using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxFreeAllowance
{
    public class BlindTaxFreeAllowance : ITaxFreeAllowance
    {
        private readonly MonetaryValue _standardTaxFreeAllowance = new MonetaryValue(15440m, Frequency.Annually);
        public MonetaryValue GetTaxFreeAllowance()
        {
            return _standardTaxFreeAllowance;
        }
    }
}
