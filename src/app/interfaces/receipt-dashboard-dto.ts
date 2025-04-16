export interface ReceiptDashboardDTO {
    receiptId: string;
    receiptNumber: string;
    receiptDate: string;
    supplierName: string;
    supplierAddress: string;
    supplierPhone: string;
    customerName: string;
    customerPhone: string;
    customerAddress: string;
    subTotal: number;
    discount: number;
    otherCharge: number;
    totalAmount: number;
}
