import { ReceiptItem } from "./receipt-item.model";

export interface ReceiptDetails {
    transactionDate: Date | null;
    transactionTime: Date | null;
    issueDate: Date | null;
    issueTime: Date | null;
    invoiceDate: Date | null;
    dueDate: Date | null;
    invoiceNumber: string | null;
    orderNumber: string | null  ;
    vendorName: string | null;
    vendorAddress: string | null;
    vendorPhone: string | null;
    vendorEmailAddress: string | null;
    vendorTIN: string | null;
    customerName: string | null;
    customerAddress: string | null;
    customerPhone: string | null;
    customerTIN: string | null;
    currency: string | null;
    exchangeRate: number;
    itemsCount: number;
    shippingFee: number;
    additionalFee: number;
    taxableAmount: number;
    discount: number;
    tip: number;
    tax: number;
    subtotal: number;
    amountDue: number;
    amountPaid: number;
    change: number;
    balanceDue: number;
    paymentMethod: string;
    paymentStatus: string;
    returnPolicy: string;
    paymentReferenceNumber: string;
    paymentTerms: string;
    table: string;
    server: string;
    station: string;
    serviceAddress: string;
    serviceStartDate: Date;
    serviceEndDate: Date;
    deliveryInfo: string;
    barcodeQRCode: string;
    unknownField: string;
    total: number;
    receiptItems: ReceiptItem[];
}
