namespace ACME.OOP.Procurement.Domain.Model.ValueObjects;

/// <summary>
/// Represents a unique identifier for a product in the Procurement bounded context.
/// Encapsulates a GUID to ensure type safety and domain integrity.
/// </summary>
public record ProductId
{
    public Guid Id { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductId"/> class with the specified GUID.
    /// </summary>
    /// <param name="id">The GUID representing the product identifier. It must not be an empty GUID.</param>
    /// <exception cref="ArgumentException">Thrown when the provided GUID is empty.</exception>
    public ProductId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Product id cannot be an empty UUID.", nameof(id));
        Id = id;
    }
    
    /// <summary>
    /// Factory method to generate a new <see cref="ProductId"/> with a unique GUID.
    /// </summary>
    /// <returns>A new instance of <see cref="ProductId"/> with a newly generated GUID.</returns>
    public static ProductId New() => new(Guid.NewGuid());
    
    /// <summary>
    /// Returns the string representation of the product identifier.
    /// </summary>
    /// <returns>A string representation of the GUID identifier.</returns>
    public override string ToString() => Id.ToString();
}