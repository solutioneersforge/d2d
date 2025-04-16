using FunctionAppDoc2Data.Models;
using System;

namespace FunctionAppDoc2Data.Respositories;
public interface IReceiptVerificationRepository
{
    ReceiptVerificationMasterDTO GetReceiptVerification(Guid receiptId);
}