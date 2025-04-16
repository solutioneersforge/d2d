using FunctionAppDoc2Data.Models;
using System;

namespace FunctionAppDoc2Data.Respositories;
public interface IExpenseTypeRepository
{
    int UpsertExpenseCategoryAndSubcategory(ExpenseTypeDTO expenseType, Guid userId);
}