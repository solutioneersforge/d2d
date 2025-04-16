import { ReceiptItemsApprovalDTO } from "./receipt-items-approval-dto";

export interface ReceiptApprovalDTO {
    receiptId: string;
    approvedBy: string;
    userId: string;
    approvedOn: string;
    merchantId: number;
    merchantName: string;
    merchantAddress: string;
    merchantPhone: string;
    merchantEmail: string;
    customerName: string;
    customerAddress: string;
    customerPhone: string;
    receiptNumber: string;
    receiptDate: string;
    subTotal: number;
    taxAmount: number;
    serviceCharge: number;
    otherCharge: number;
    statusId: number;
    totalAmount: number;
    discount: number;
    receiptItemsApproval: ReceiptItemsApprovalDTO[];
}
