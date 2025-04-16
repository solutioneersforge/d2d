using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class ExpenseCategoriesMapper
{
    public static IEnumerable<ExpenseCategoriesDTO> MapToExpenseCategoriesDTO(this IEnumerable<ExpenseCategory> expenseCategories)
    {
        try
        {
            if (expenseCategories == null)
                throw new ArgumentNullException(nameof(expenseCategories));

            return expenseCategories.Select(expenseCategory => new ExpenseCategoriesDTO
            {
                CategoryId = expenseCategory.CategoryId,
                CategoryName = expenseCategory.CategoryName,
                IsActive = expenseCategory.IsActive.GetValueOrDefault(),
                ExpenseSubCategoriesDTOs = expenseCategory.ExpenseSubCategories.Select(subCategory => new ExpenseSubCategoriesDTO
                {
                    CategoryId = subCategory.CategoryId,
                    IsActive = subCategory.IsActive.GetValueOrDefault(),
                    SubCategoryId = subCategory.SubCategoryId,
                    SubCategoryName = subCategory.SubCategoryName
                })
            });
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
