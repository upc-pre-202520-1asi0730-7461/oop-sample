using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents a purchase order aggregate for the Procurement bounded context.
/// Encapsulates information about the supplier, order date, currency, and line items.
/// </summary>
/// <param name="orderNumber">The unique order number for the purchase order.</param>
/// <param name="supplierId">The <see cref="SupplierId"/> unique identifier of the supplier.</param>
/// <param name="orderDate">The date the order was placed.</param>
/// <param name="currency">The 3-letter ISO currency code for the order (e.g., "USD", "EUR").</param>
public class PurchaseOrder(string orderNumber, SupplierId supplierId, DateTime orderDate, string currency)
{
    private readonly List<PurchaseOrderItem> _items = new();
    public string OrderNumber { get; } = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber));
    public SupplierId SupplierId { get; } = supplierId ?? throw new ArgumentNullException(nameof(supplierId));
    public DateTime OrderDate { get; } = orderDate;
    public string Currency { get; } = string.IsNullOrWhiteSpace(currency) || currency.Length != 3 
        ? throw new ArgumentException("Currency must be a valid 3-letter ISO code.", nameof(currency)) 
        : currency.ToUpper();
    public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Adds a new item to the purchase order.
    /// </summary>
    /// <remarks>
    /// The method validates the input parameters to ensure that the quantity is greater than zero and the unit price is a non-negative number.
    /// If the parameters are valid, a new <see cref="PurchaseOrderItem"/> is created and added to the internal list of items.
    /// </remarks>
    /// <param name="productId">The <see cref="ProductId"/> unique identifier of the product being ordered.</param>
    /// <param name="quantity">The quantity of the product being ordered. Must be greater than zero.</param>
    /// <param name="unitPriceAmount">The unit price of the product as a decimal. Must be a non-negative number.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if quantity is less than or equal to zero or if unit price is negative.</exception>
    public PurchaseOrder AddItem(ProductId productId, int quantity, decimal unitPriceAmount)
    {
        ArgumentNullException.ThrowIfNull(productId);
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        if (unitPriceAmount < 0) throw new ArgumentOutOfRangeException(nameof(unitPriceAmount), "Unit price must be a non-negative number.");
        
        var unitPrice = new Money(unitPriceAmount, Currency);
        var item = new PurchaseOrderItem(productId, quantity, unitPrice);
        _items.Add(item);
        return this;
    }

    /// <summary>
    /// Calculates the total amount for the entire purchase order by summing the totals of all individual items.
    /// </summary>
    /// <returns>A <see cref="Money"/> representing the total amount for the purchase order.</returns>
    public Money CalculateOrderTotal()
    {
        var total = _items.Sum(item => item.CalculateItemTotal().Amount);
        return new Money(total, Currency);
    }
}
