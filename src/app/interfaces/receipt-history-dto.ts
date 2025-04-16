export interface ReceiptHistoryDTO {
  receiptId: string;
  receiptNumber: string;
  receiptDate: Date;
  merchantName: string;
  merchantAddress: string;
  customerName: string;
  customerAddress: string;
  customerPhone: string;
  subTotalAmount?: number;
  totalAmount?: number;
  taxAmount?: number;
  otherCharge?: number;
  statusId: number;
  statusName: string;
  serviceChargeAmount?: number;
}
