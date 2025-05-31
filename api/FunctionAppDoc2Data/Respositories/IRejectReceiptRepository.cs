using FunctionAppDoc2Data.Models;
using System;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IRejectReceiptRepository
{
    Task<int> RejectReceipt(RejectReceiptDTO rejectReceipt, Guid userId);
}