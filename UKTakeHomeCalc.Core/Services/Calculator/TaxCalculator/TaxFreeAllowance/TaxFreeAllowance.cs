using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxFreeAllowance
{
    public class TaxFreeAllowance : ITaxFreeAllowance
    {
        private readonly MonetaryValue _taxFreeAllowance;
        public TaxFreeAllowance(MonetaryValue taxFreeAllowance)
        {
            _taxFreeAllowance = taxFreeAllowance;
        }

        public MonetaryValue GetTaxFreeAllowance()
        {
            return _taxFreeAllowance;
        }
    }
}
