namespace UKTakeHomeCalc.Core.Models;
public struct MonetaryValue
{
    public decimal Value { get; }
    public Frequency ValueFrequency { get; }
    private decimal BaseValue
    {
        get
        {
            if (Value == 0) return 0;

            return Value / (int)ValueFrequency;
        }
    }

    public MonetaryValue()
    {
        Value = 0;
        ValueFrequency = Frequency.Weekly;
    }

    public MonetaryValue(decimal value, Frequency valueFrequency)
    {
        Value = Math.Round(value, 8);
        ValueFrequency = valueFrequency;
    }

    public static MonetaryValue operator +(MonetaryValue value1, MonetaryValue value2)
    {
        return new MonetaryValue((value1.BaseValue + value2.BaseValue) * (int)value1.ValueFrequency, value1.ValueFrequency);
    }

    public static MonetaryValue operator *(MonetaryValue value1, decimal value2)
    {
        return new MonetaryValue(value1.Value * value2, value1.ValueFrequency);
    }

    public static MonetaryValue operator -(MonetaryValue value1, MonetaryValue value2)
    {
        return value1 + value2 * -1;
    }

    public static MonetaryValue operator -(MonetaryValue value1)
    {
        return value1 * -1;
    }

    public static MonetaryValue operator /(MonetaryValue value1, decimal value2)
    {
        if (value2 == 0)
            throw new DivideByZeroException("Cannot divide a MonetaryValue by zero.");

        return new MonetaryValue(value1.Value / value2, value1.ValueFrequency);
    }

    public static bool operator <(MonetaryValue value1, MonetaryValue value2)
    {
        return Math.Round(value1.BaseValue, 2) < Math.Round(value2.BaseValue, 2);
    }

    public static bool operator >(MonetaryValue value1, MonetaryValue value2)
    {
        return Math.Round(value1.BaseValue, 2) > Math.Round(value2.BaseValue, 2);
    }

    public static bool operator <=(MonetaryValue value1, MonetaryValue value2)
    {
        return Math.Round(value1.BaseValue, 2) <= Math.Round(value2.BaseValue, 2);
    }

    public static bool operator >=(MonetaryValue value1, MonetaryValue value2)
    {
        return Math.Round(value1.BaseValue, 2) >= Math.Round(value2.BaseValue, 2);
    }

    public static implicit operator MonetaryValue(decimal v)
    {
        return new MonetaryValue(v, Frequency.Weekly);
    }

    public override bool Equals(object? value)
    {
        if (value == null) return false;

        if (Math.Round(BaseValue, 2) == Math.Round(((MonetaryValue)value).BaseValue, 2))
            return true;

        return false;
    }

    public override int GetHashCode()
    {
        int initialHash = 23;

        unchecked
        {
            initialHash = initialHash * 23 + Value.GetHashCode();
            initialHash = initialHash * 23 + ValueFrequency.GetHashCode();
        }

        return initialHash;
    }

    public static bool operator ==(MonetaryValue value1, MonetaryValue value2)
    {
        return value1.Equals(value2);
    }

    public static bool operator !=(MonetaryValue value1, MonetaryValue value2)
    {
        return !value1.Equals(value2);
    }

    public override string ToString()
    {
        return $"{Math.Round(Value, 2):n2}/{ValueFrequency}";
    }
}