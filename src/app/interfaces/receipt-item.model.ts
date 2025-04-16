import { ExpenseSubCategoriesDTO } from "./expense-sub-categories-dto";

export interface ReceiptItem {
  description: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  discount: number;
  subcategoryId: number ;
}
