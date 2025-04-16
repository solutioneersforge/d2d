using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class ExpenseTypeMapper
{
    public static ExpenseCategory MapToExpenseCategory(this ExpenseTypeDTO expenseType, List<ExpenseCategory> expenseCategories)
    {
        if (expenseType == null)
            throw new ArgumentNullException(nameof(expenseType));

        if (expenseCategories == null)
            throw new ArgumentNullException(nameof(expenseCategories));

        var existingCategory = expenseCategories.FirstOrDefault(c => c.CategoryName == expenseType.CategoryName);

        if (existingCategory != null)
        {
            if (!existingCategory.ExpenseSubCategories.Any(sc => sc.SubCategoryName == expenseType.SubCategoryName))
            {
                existingCategory.ExpenseSubCategories.Add(new ExpenseSubCategory
                {
                    SubCategoryName = expenseType.SubCategoryName,
                    CategoryId = existingCategory.CategoryId
                });
            }
            return existingCategory;
        }
        else
        {
            var newCategory = new ExpenseCategory
            {
                CategoryName = expenseType.CategoryName,
                ExpenseSubCategories = new List<ExpenseSubCategory>
                {
                    new ExpenseSubCategory
                    {
                        SubCategoryName = expenseType.SubCategoryName
                    }
                }
            };
            expenseCategories.Add(newCategory);
            return newCategory;
        }
    }
}
