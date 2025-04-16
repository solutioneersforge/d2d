using FunctionAppDoc2Data.Models;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IReceiptRespository
{
    Task<int> CreateReceipt(ReceiptMasterDTO receiptMaster);
}