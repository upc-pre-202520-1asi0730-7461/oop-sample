using ACME.OOP.Procurement.Domain.Model.Aggregates;
using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.Aggregates;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

// Example usage of the PurchaseOrder aggregate
// Creating a supplier and a purchase order with items
var supplierAddress = new Address("Supplier St", "123", "SupplierCity", "Supplier State", "12345", "United States");
var supplier = new Supplier("SUP001", "Microsoft, Inc.", supplierAddress);

// Create a new purchase order for the supplier
var purchaseOrder = new PurchaseOrder("PO001", new SupplierId(supplier.Identifier), DateTime.Now, "USD");
// Add items to the purchase order
purchaseOrder.AddItem(ProductId.New(), 10, 25.99m)
             .AddItem(ProductId.New(), 20, 19.99m);
Console.WriteLine($"Purchase Order {purchaseOrder.OrderNumber} created for Supplier {supplier.Name} in {purchaseOrder.Currency}");
// Calculate and display the total amounts
foreach (var item in purchaseOrder.Items)
{
    Console.WriteLine($"Order Item Total: {item.CalculateItemTotal()}");
}
Console.WriteLine($"Total Order Amount: {purchaseOrder.CalculateOrderTotal()}");