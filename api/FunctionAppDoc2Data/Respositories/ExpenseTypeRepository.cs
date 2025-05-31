using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Respositories;
public class ExpenseTypeRepository : IExpenseTypeRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly ILogger<ExpenseTypeRepository> _logger;

    public ExpenseTypeRepository(DocToDataDBContext docToDataDBContext, ILogger<ExpenseTypeRepository> logger)
    {
        _docToDataDBContext = docToDataDBContext;
        _logger = logger;
    }
    public int UpsertExpenseCategoryAndSubcategory(ExpenseTypeDTO expenseType, Guid userId)
    {
        if (expenseType == null) throw new ArgumentNullException(nameof(expenseType));

        if (expenseType.SubcategoryId.HasValue && expenseType.SubcategoryId != 0)
        {
            var subCategory = _docToDataDBContext.ExpenseSubCategories
                        .FirstOrDefault(m => m.SubCategoryId == expenseType.SubcategoryId);
            subCategory.SubCategoryName = expenseType.SubCategoryName;
            subCategory.IsActive = expenseType.IsActive;
            _docToDataDBContext.SaveChanges();
            return 3;
        }

        if (!expenseType.CategoryId.HasValue || expenseType.CategoryId == 0)
        {
            if (_docToDataDBContext.ExpenseCategories.Any(m => m.CategoryName == expenseType.CategoryName))
            {
                return 0;
            }
        }

        else
        {
            if (_docToDataDBContext.ExpenseSubCategories.Any(m => m.CategoryId == expenseType.CategoryId
                                && m.SubCategoryName == expenseType.SubCategoryName))
            {
                return 0;
            }
        }

        using var transaction = _docToDataDBContext.Database.BeginTransaction();
        try
        {
            int result = 0;
            var category = GetOrCreateCategory(expenseType, userId);

            if (!string.IsNullOrEmpty(expenseType.SubCategoryName))
            {
                UpsertSubCategory(expenseType, category.CategoryId);
            }

            result = _docToDataDBContext.SaveChanges();
            transaction.Commit();
            return result;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError($"ArgumentNullException: {ex.Message}", ex);
            throw;
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError($"KeyNotFoundException: {ex.Message}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error: {ex.Message}", ex);
            return 0;
        }
    }


    private ExpenseCategory GetOrCreateCategory(ExpenseTypeDTO expenseType, Guid userId)
    {
        var category = expenseType.CategoryId.HasValue && expenseType.CategoryId > 0
            ? _docToDataDBContext.ExpenseCategories.Find(expenseType.CategoryId)
            : null;

        if (category == null)
        {
            category = new ExpenseCategory { CategoryName = expenseType.CategoryName, UserId = userId };
            _docToDataDBContext.ExpenseCategories.Add(category);
            _docToDataDBContext.SaveChanges();
        }
        else if (!string.IsNullOrEmpty(expenseType.CategoryName))
        {
            category.CategoryName = expenseType.CategoryName;
        }

        return category;
    }

    private void UpsertSubCategory(ExpenseTypeDTO expenseType, int categoryId)
    {
        var subCategory = expenseType.SubcategoryId.HasValue && expenseType.SubcategoryId > 0
            ? _docToDataDBContext.ExpenseSubCategories.Find(expenseType.SubcategoryId)
            : null;

        if (subCategory == null)
        {
            _docToDataDBContext.ExpenseSubCategories.Add(new ExpenseSubCategory
            {
                CategoryId = categoryId,
                SubCategoryName = expenseType.SubCategoryName
            });
        }
        else
        {
            subCategory.SubCategoryName = expenseType.SubCategoryName;
        }
    }


}
