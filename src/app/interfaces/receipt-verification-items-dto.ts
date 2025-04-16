export interface ReceiptVerificationItemsDTO {
    receiptItemID: string | ''; 
    itemDescription: string | '';
    quantity: number | 0;
    unitPrice: number | 0;
    unitOfMeasureId: number | 0;
    discount: number | 0;
    total: number | 0;
    subCategoryId: number | null;
    subCategoryName: string | null;
}
