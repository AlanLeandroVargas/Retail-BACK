namespace Domain.Entities;

public class SaleProduct
{
    public SaleProduct()
    {
    }

    public SaleProduct(Guid productId, int saleId, int quantity, decimal price, int discount)
    {
        Product = productId;
        Sale = saleId;
        Quantity = quantity;
        Price = price;
        Discount = discount;
    }
    public int ShoppingCartId {get;set;}
    public Sale SaleInstance {get;set;}
    public int Sale {get;set;}
    public Product ProductInstance {get;set;}
    public Guid Product {get;set;}
    public int Quantity {get;set;}
    public decimal Price {get;set;}
    public int Discount {get;set;}
}
