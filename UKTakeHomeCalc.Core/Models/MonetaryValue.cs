namespace UKTakeHomeCalc.Core.Models;

public class MonetaryValue
{
    private decimal _value;
    public decimal Value { get => Math.Round(_value, 2); set => _value = value; }
    public Frequency ValueFrequency { get; set; }
    private decimal BaseValue { get => Math.Round(_value / (int)ValueFrequency, 4); }
    public MonetaryValue(decimal value, Frequency valueFrequency)
    {
        Value = value;
        ValueFrequency = valueFrequency;
    }
    public static MonetaryValue operator+(MonetaryValue value1, MonetaryValue value2)
    {
        return new MonetaryValue((value1.BaseValue + value2.BaseValue) * (int)value1.ValueFrequency, value1.ValueFrequency);
    }
    public static MonetaryValue operator*(MonetaryValue value1, decimal value2)
    {
        return new MonetaryValue(value1.Value * value2, value1.ValueFrequency);
    }
    public override bool Equals(object? value)
    {
        if (value == null) return false;

        if(BaseValue == ((MonetaryValue)value).BaseValue)
            return true;

        return false;
    }
}