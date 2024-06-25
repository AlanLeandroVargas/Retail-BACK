namespace Domain.Entities;

public class Category
{
    public Category(){

    }
    public Category(int categoryId, string name)
    {
        CategoryId = categoryId;
        Name = name;
    }
    public int CategoryId {get;set;}
    public string Name {get;set;}
    public IList<Product> Products {get;set;}
}
