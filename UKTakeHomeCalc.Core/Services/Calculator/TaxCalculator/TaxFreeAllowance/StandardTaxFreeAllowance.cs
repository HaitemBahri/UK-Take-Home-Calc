using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.Services.Calculator.TaxCalculator.TaxFreeAllowance
{
    public class StandardTaxFreeAllowance : ITaxFreeAllowance
    {
        private readonly MonetaryValue _standardTaxFreeAllowance = new MonetaryValue(12570m, Frequency.Annually);
        public MonetaryValue GetTaxFreeAllowance()
        {
            return _standardTaxFreeAllowance;
        }
    }
}
