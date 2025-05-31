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
  approvedBy?: string;
  approvedOn?: Date;
  modifiedBy?: string;
  modifiedOn?: Date;
  createdBy: string;
  createdOn? : Date;
  remarks?: string;
  currencyCode? : string;
  selectedOption?: string;
}
