namespace Subscriptions.Domain;

public readonly struct Money
{
    public int WholePart { get; }
    public int DecimalPart { get; }

    public Money(int wholePart, int decimalPart)
    {
        if (decimalPart < 0 || decimalPart > 99)
            throw new ArgumentOutOfRangeException(nameof(decimalPart), "Decimal part must be between 0 and 99.");

        WholePart = wholePart;
        DecimalPart = decimalPart;
    }

    public Money Add(Money other)
    {
        var newWholePart = WholePart + other.WholePart;
        var newDecimalPart = DecimalPart + other.DecimalPart;

        if (newDecimalPart >= 100)
        {
            newWholePart += 1;
            newDecimalPart -= 100;
        }

        return new Money(newWholePart, newDecimalPart);
    }

    public Money Subtract(Money other)
    {
        var newWholePart = WholePart - other.WholePart;
        var newDecimalPart = DecimalPart - other.DecimalPart;

        if (newDecimalPart < 0)
        {
            newWholePart -= 1;
            newDecimalPart += 100;
        }

        if (newWholePart < 0) throw new InvalidOperationException("Resulting money amount cannot be negative.");

        return new Money(newWholePart, newDecimalPart);
    }

    public override string ToString()
    {
        return $"{WholePart}.{DecimalPart:D2}";
    }

    public static Money FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

        var parts = value.Split('.', StringSplitOptions.RemoveEmptyEntries);
        return new Money(int.Parse(parts[0]), int.Parse(parts[1]));
    }
}