﻿namespace Application.Request;

public class UpdateSaleRequest
{
    public int SaleId {get;set;}
    public decimal TotalPay {get;set;}
    public decimal Subtotal {get;set;}
    public decimal TotalDiscount {get;set;}
    public decimal Taxes {get;set;}
    public DateTime Date {get;set;}
}
