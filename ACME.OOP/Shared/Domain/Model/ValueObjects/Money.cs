namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a monetary value with an amount and currency.
/// </summary>
public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="Money"/> class.
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currency">The currency code (ISO 4217 format).</param>
    /// <exception cref="ArgumentException">Thrown when amount is negative or currency is invalid.</exception>
    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new ArgumentException("Currency must be a valid 3-letter ISO code.", nameof(currency));
        if (amount < 0)
            throw new ArgumentException("Amount must be a non-negative number.", nameof(amount));
        Amount = amount;
        Currency = currency.ToUpper();
    }
    
    /// <summary>
    /// Returns a string representation of the monetary value.
    /// </summary>
    /// <returns>A string in the format "Amount Currency".</returns>
    public override string ToString() => $"{Amount} {Currency}";
}