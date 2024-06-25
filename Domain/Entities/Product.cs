namespace Domain.Entities;

public class Product
{
    public Product()
    {
    }
    public Product(Guid productId, string name, string description, decimal price,int categoryId, int discount ,string imageurl)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
        Category = categoryId;
        Discount = discount;
        ImageUrl = imageurl;
    }

    public Guid ProductId {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public decimal Price {get;set;}
    public Category CategoryInstance {get;set;}
    public int Category {get;set;}
    public int Discount {get;set;}
    public string ImageUrl {get;set;}
    public IList<SaleProduct> SaleProducts {get;set;}
}
