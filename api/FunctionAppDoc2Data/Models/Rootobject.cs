using System;


namespace FunctionAppDoc2Data.Models;
public class Rootobject
{
    public string status { get; set; }
    public DateTime createdDateTime { get; set; }
    public DateTime lastUpdatedDateTime { get; set; }
    public Analyzeresult analyzeResult { get; set; }
}

public class Analyzeresult
{
    public string apiVersion { get; set; }
    public string modelId { get; set; }
    public string stringIndexType { get; set; }
    public string content { get; set; }
    public Page[] pages { get; set; }
    public Table[] tables { get; set; }
    public Paragraph[] paragraphs { get; set; }
    public Style[] styles { get; set; }
    public Document[] documents { get; set; }
    public string contentFormat { get; set; }
    public Section[] sections { get; set; }
    public Figure[] figures { get; set; }
}

public class Page
{
    public int pageNumber { get; set; }
    public float angle { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public string unit { get; set; }
    public Word[] words { get; set; }
    public Selectionmark[] selectionMarks { get; set; }
    public Line[] lines { get; set; }
    public Span3[] spans { get; set; }
}

public class Word
{
    public string content { get; set; }
    public int[] polygon { get; set; }
    public float confidence { get; set; }
    public Span span { get; set; }
}

public class Span
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Selectionmark
{
    public string state { get; set; }
    public int[] polygon { get; set; }
    public float confidence { get; set; }
    public Span1 span { get; set; }
}

public class Span1
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Line
{
    public string content { get; set; }
    public int[] polygon { get; set; }
    public Span2[] spans { get; set; }
}

public class Span2
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Span3
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Table
{
    public int rowCount { get; set; }
    public int columnCount { get; set; }
    public Cell[] cells { get; set; }
    public Boundingregion1[] boundingRegions { get; set; }
    public Span5[] spans { get; set; }
}

public class Cell
{
    public string kind { get; set; }
    public int rowIndex { get; set; }
    public int columnIndex { get; set; }
    public string content { get; set; }
    public Boundingregion[] boundingRegions { get; set; }
    public Span4[] spans { get; set; }
    public string[] elements { get; set; }
    public int columnSpan { get; set; }
    public int rowSpan { get; set; }
}

public class Boundingregion
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span4
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Boundingregion1
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span5
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Paragraph
{
    public Span6[] spans { get; set; }
    public Boundingregion2[] boundingRegions { get; set; }
    public string content { get; set; }
}

public class Span6
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Boundingregion2
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Style
{
    public float confidence { get; set; }
    public Span7[] spans { get; set; }
    public bool isHandwritten { get; set; }
}

public class Span7
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Document
{
    public string docType { get; set; }
    public Boundingregion19[] boundingRegions { get; set; }
    public Fields fields { get; set; }
    public float confidence { get; set; }
    public Span24[] spans { get; set; }
}

public class Fields
{
    public Transactiondate TransactionDate { get; set; }
    public Vendorname VendorName { get; set; }
    public Vendorphone VendorPhone { get; set; }
    public Customeraddress CustomerAddress { get; set; }
    public Subtotal Subtotal { get; set; }
    public Tax Tax { get; set; }
    public Items Items { get; set; }
    public Ordernumber OrderNumber { get; set; }
    public Vendoraddress VendorAddress { get; set; }
    public Vendoremailaddress VendorEmailAddress { get; set; }
    public Customername CustomerName { get; set; }
    public Total Total { get; set; }
    public Returnpolicy ReturnPolicy { get; set; }
    public Invoicenumber InvoiceNumber { get; set; }
    public Transactiontime TransactionTime { get; set; }
    public Issuedate IssueDate { get; set; }
    public Issuetime IssueTime { get; set; }
    public Invoicedate InvoiceDate { get; set; }
    public Duedate DueDate { get; set; }
    public Vendortin VendorTIN { get; set; }
    public Customerphone CustomerPhone { get; set; }
    public Customertin CustomerTIN { get; set; }
    public Currency Currency { get; set; }
    public Exchangerate ExchangeRate { get; set; }
    public Itemscount ItemsCount { get; set; }
    public Shippingfee ShippingFee { get; set; }
    public Additionalfree AdditionalFree { get; set; }
    public Taxableamount TaxableAmount { get; set; }
    public Discount Discount { get; set; }
    public Tip Tip { get; set; }
    public Amountdue AmountDue { get; set; }
    public Amountpaid AmountPaid { get; set; }
    public Change Change { get; set; }
    public Balancedue BalanceDue { get; set; }
    public Paymentmethod PaymentMethod { get; set; }
    public Paymentstatus PaymentStatus { get; set; }
    public Paymentreferencenumber PaymentReferenceNumber { get; set; }
    public Paymentterms PaymentTerms { get; set; }
    public Table1 Table { get; set; }
    public Server Server { get; set; }
    public Station Station { get; set; }
    public Serviceaddress ServiceAddress { get; set; }
    public Servicestartdate ServiceStartDate { get; set; }
    public Serviceenddate ServiceEndDate { get; set; }
    public Deliveryinfo DeliveryInfo { get; set; }
    public Barcodeqrcode BarcodeQRCode { get; set; }
    public Unknownfield UnknownField { get; set; }
}

public class Transactiondate
{
    public string type { get; set; }
    public DateTime valueDate { get; set; }
    public string content { get; set; }
    public Boundingregion3[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span8[] spans { get; set; }
}

public class Boundingregion3
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span8
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Vendorname
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public Boundingregion4[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span9[] spans { get; set; }
}

public class Boundingregion4
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span9
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Vendorphone
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public Boundingregion5[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span10[] spans { get; set; }
}

public class Boundingregion5
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span10
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Customeraddress
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public Boundingregion6[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span11[] spans { get; set; }
}

public class Boundingregion6
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span11
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Subtotal
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public Boundingregion7[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span12[] spans { get; set; }
}

public class Boundingregion7
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span12
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Tax
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public Boundingregion8[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span13[] spans { get; set; }
}

public class Boundingregion8
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span13
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Items
{
    public string type { get; set; }
    public Valuearray[] valueArray { get; set; }
    public float confidence { get; set; }
}

public class Valuearray
{
    public string type { get; set; }
    public Valueobject valueObject { get; set; }
    public float confidence { get; set; }
}

public class Valueobject
{
    public Description Description { get; set; }
    public Quantity Quantity { get; set; }
    public Totalprice TotalPrice { get; set; }
    public Unitprice UnitPrice { get; set; }
    public COLUMN1 COLUMN1 { get; set; }
    public COLUMN2 COLUMN2 { get; set; }
    public Price Price { get; set; }
    public Amount Amount { get; set; }
    public Quantityunit QuantityUnit { get; set; }
}

public class Description
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public Boundingregion9[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span14[] spans { get; set; }
}

public class Boundingregion9
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span14
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Quantity
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public Boundingregion10[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span15[] spans { get; set; }
}

public class Boundingregion10
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span15
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Totalprice
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public Boundingregion11[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span16[] spans { get; set; }
}

public class Boundingregion11
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span16
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Unitprice
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public Boundingregion12[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span17[] spans { get; set; }
}

public class Boundingregion12
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span17
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class COLUMN1
{
    public string type { get; set; }
    public float confidence { get; set; }
}

public class COLUMN2
{
    public string type { get; set; }
    public float confidence { get; set; }
}

public class Price
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Amount
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Quantityunit
{
    public string type { get; set; }
    public float confidence { get; set; }
}

public class Ordernumber
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public Boundingregion13[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span18[] spans { get; set; }
}

public class Boundingregion13
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span18
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Vendoraddress
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public Boundingregion14[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span19[] spans { get; set; }
}

public class Boundingregion14
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span19
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Vendoremailaddress
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public Boundingregion15[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span20[] spans { get; set; }
}

public class Boundingregion15
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span20
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Customername
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public Boundingregion16[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span21[] spans { get; set; }
}

public class Boundingregion16
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span21
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Total
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public Boundingregion17[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span22[] spans { get; set; }
}

public class Boundingregion17
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span22
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Returnpolicy
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public Boundingregion18[] boundingRegions { get; set; }
    public float confidence { get; set; }
    public Span23[] spans { get; set; }
}

public class Boundingregion18
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span23
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Invoicenumber
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Transactiontime
{
    public string type { get; set; }
    public DateTime valueDate { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Issuedate
{
    public string type { get; set; }
    public DateTime valueDate { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Issuetime
{
    public string type { get; set; }
    public DateTime valueDate { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Invoicedate
{
    public string type { get; set; }
    public DateTime valueDate { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Duedate
{
    public string type { get; set; }
    public DateTime valueDate { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Vendortin
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Customerphone
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Customertin
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Currency
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Exchangerate
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Itemscount
{
    public string type { get; set; }
    public int valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Shippingfee
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Additionalfree
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Taxableamount
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Discount
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Tip
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Amountdue
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Amountpaid
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Change
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Balancedue
{
    public string type { get; set; }
    public decimal valueNumber { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Paymentmethod
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Paymentstatus
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Paymentreferencenumber
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Paymentterms
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Table1
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Server
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Station
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Serviceaddress
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Servicestartdate
{
    public string type { get; set; }
    public DateTime valueDate { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Serviceenddate
{
    public string type { get; set; }
    public DateTime valueDate { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Deliveryinfo
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Barcodeqrcode
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Unknownfield
{
    public string type { get; set; }
    public string valueString { get; set; }
    public string content { get; set; }
    public float confidence { get; set; }
}

public class Boundingregion19
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span24
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Section
{
    public Span25[] spans { get; set; }
    public string[] elements { get; set; }
}

public class Span25
{
    public int offset { get; set; }
    public int length { get; set; }
}

public class Figure
{
    public string id { get; set; }
    public Boundingregion20[] boundingRegions { get; set; }
    public Span26[] spans { get; set; }
    public string[] elements { get; set; }
}

public class Boundingregion20
{
    public int pageNumber { get; set; }
    public int[] polygon { get; set; }
}

public class Span26
{
    public int offset { get; set; }
    public int length { get; set; }
}

