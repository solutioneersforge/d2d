import { ExpenseSubCategoriesDTO } from "./expense-sub-categories-dto";

export interface ExpenseCategoriesDTO {
    categoryId: number;
    categoryName: string;
    expenseSubCategoriesDTOs : ExpenseSubCategoriesDTO[];
}
