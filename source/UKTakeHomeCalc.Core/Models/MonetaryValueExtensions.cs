namespace UKTakeHomeCalc.Core.Models
{
    public static class MonetaryValueExtensions
    {
        public static MonetaryValue Weekly(this decimal value)
        {
            return new MonetaryValue(value, Frequency.Weekly);
        }

        public static MonetaryValue Monthly(this decimal value)
        {
            return new MonetaryValue(value, Frequency.Monthly);
        }

        public static MonetaryValue Annually(this decimal value)
        {
            return new MonetaryValue(value, Frequency.Annually);
        }

        public static MonetaryValue Every(this decimal value, Frequency frequency)
        {
            return new MonetaryValue(value, frequency);
        }
    }
}
