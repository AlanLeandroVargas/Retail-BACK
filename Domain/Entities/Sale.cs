namespace Domain.Entities;

public class Sale
{
    public Sale()
    {
    }

    public Sale(decimal totalpay, decimal subtotal, decimal totalDiscount, decimal taxes, DateTime date)
    {
        TotalPay = totalpay;
        Subtotal = subtotal;
        TotalDiscount = totalDiscount;
        Taxes = taxes;
        Date = date;
    }
    public int SaleId {get;set;}
    public decimal TotalPay {get;set;}
    public decimal Subtotal {get;set;}
    public decimal TotalDiscount {get;set;}
    public decimal Taxes {get;set;}
    public DateTime Date {get;set;}
    public IList<SaleProduct> SaleProducts {get;set;}
}
