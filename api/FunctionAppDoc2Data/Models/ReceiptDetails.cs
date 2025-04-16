using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.Models;
public class ReceiptDetails
{
    public DateTime TransactionDate { get; set; }
    public DateTime TransactionTime { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime IssueTime { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public string InvoiceNumber { get; set; }
    public string OrderNumber { get; set; }
    public string VendorName { get; set; }
    public string VendorAddress { get; set; }
    public string VendorPhone { get; set; }
    public string VendorEmailAddress { get; set; }
    public string VendorTIN { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerTIN { get; set; }
    public string Currency { get; set; }
    public decimal ExchangeRate { get; set; }
    public int ItemsCount { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal AdditionalFree { get; set; }
    public decimal TaxableAmount { get; set; }
    public decimal Discount { get; set; }
    public decimal Tip { get; set; }
    public decimal Tax { get; set; }
    public decimal Subtotal { get; set; }
    public decimal AmountDue { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal Change { get; set; }
    public decimal BalanceDue { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }
    public string ReturnPolicy { get; set; }
    public string PaymentReferenceNumber { get; set; }
    public string PaymentTerms { get; set; }
    public string Table { get; set; }
    public string Server { get; set; }
    public string Station { get; set; }
    public string ServiceAddress { get; set; }
    public DateTime ServiceStartDate { get; set; }
    public DateTime ServiceEndDate { get; set; }
    public string DeliveryInfo { get; set; }
    public string BarcodeQRCode { get; set; }
    public string UnknownField { get; set; }
    public decimal Total { get; set; }
    public List<ReceiptItem> ReceiptItems { get; set; }

}

public class ReceiptItem
{
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
}
