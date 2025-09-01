namespace ACME.OOP.SCM.Domain.Model.ValueObjects;

/// <summary>
/// Represents a unique identifier for a supplier in the Supply Chain Management bounded context.
/// </summary>
public record SupplierId
{
    public string Identifier { get; init; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SupplierId"/> value object.
    /// </summary>
    /// <param name="identifier"></param>
    /// <exception cref="ArgumentException"></exception>
    public SupplierId(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            throw new ArgumentException("Supplier id cannot be null or whitespace.", nameof(identifier));
        Identifier = identifier;
    }
    
    /// <summary>
    /// Returns the string representation of the SupplierId.
    /// </summary>
    /// <returns>A string that represents the SupplierId.</returns>
    public override string ToString() => Identifier;
}