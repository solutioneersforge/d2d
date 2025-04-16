import { ReceiptVerificationItemsDTO } from "./receipt-verification-items-dto";

export interface ReceiptVerificationMasterDTO {
    receiptId: string;
    userId: string;
    vendorName: string;
    vendorAddress: string;
    vendorPhone: string;
    vendorEmail: string;
    customerName: string;
    customerAddress: string;
    customerPhone: string;
    invoiceNumber: string;
    invoiceDate: string;
    subTotal: number;
    taxAmount: number;
    total: number;
    imagePath: string;
    image: string;
    originalFileName: string;
    isImage: boolean;
    receiptVerificationItems: ReceiptVerificationItemsDTO[] ;
}
