export interface ReceiptItemsApprovalDTO {
    receiptItemId: string;
    receiptId: string;
    itemDescription: string;
    quantity: number;
    unitPrice: number;
    discount: number;
    total: number;
    subCategoryId?: number  ;
    unitOfMeasureId?: number;
}
