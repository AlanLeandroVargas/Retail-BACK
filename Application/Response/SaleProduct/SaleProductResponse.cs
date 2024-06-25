namespace Application.Response;

public class SaleProductResponse
{
    public int Id {get;set;}
    public Guid ProductId {get;set;}
    public int Quantity {get;set;}
    public decimal Price {get;set;}
    public decimal Discount {get;set;}
}
