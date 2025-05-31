using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.Respositories;
public interface IReceiptHistoryRepository
{
    IEnumerable<ReceiptHistoryDTO> GetReceiptHistory(Guid userId, DateTime fromDate, DateTime toDate);
}