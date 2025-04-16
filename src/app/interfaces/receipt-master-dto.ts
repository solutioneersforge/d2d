import { ReceiptItemDTO } from "./receipt-item-dto";

export interface ReceiptMasterDTO {
    userId: string | null;
    vendorName: string | null;
    vendorAddress: string | null;
    vendorPhone: string | null;
    vendorEmail: string | null;
    customerName: string | null;
    customerAddress: string | null;
    customerPhone: string | null;
    invoiceNumber: string | null;
    invoiceDate: Date | null;
    subTotal: number | null;
    taxAmount: number | null;
    total: number | null;
    ReceiptItemDTOs : ReceiptItemDTO[] | null;
}
