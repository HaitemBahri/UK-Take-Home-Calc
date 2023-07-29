namespace UKTakeHomeCalc.Core.Models
{
    public static class FreeAllowances
    {
        public static class Tax
        {
            public static readonly MonetaryValue StandardTaxFreeAllowance = 12570m.Annually();
            public static readonly MonetaryValue BlindTaxFreeAllowance = 15440m.Annually();
            public static readonly MonetaryValue MarriageTaxFreeAllowance = 0m.Weekly();
        }

        public static class NationalInsurance
        {
            public static readonly MonetaryValue StandardNationalInsuranceFreeAllowance = 242m.Weekly();
        }

        public static class Pension
        {
            public static readonly MonetaryValue StandardPensionFreeAllowance = 6240m.Annually();
        }
    }
}