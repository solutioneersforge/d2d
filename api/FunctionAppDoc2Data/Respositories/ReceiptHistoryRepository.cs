using FunctionAppDoc2Data.Constants;
using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Respositories;
public class ReceiptHistoryRepository : IReceiptHistoryRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    public ReceiptHistoryRepository(DocToDataDBContext docToDataDBContext)
    {
        _docToDataDBContext = docToDataDBContext;
    }

    public IEnumerable<ReceiptHistoryDTO> GetReceiptHistory(Guid userId)
    {
        var companyMember = _docToDataDBContext.CompanyMembers
                              .FirstOrDefault(m => m.UserId == userId);

        List<Guid> listOfUserId;

        if (companyMember != null && (companyMember.RoleId.ToString().ToLower() == RolesConstant.Manager.ToLower() 
            || companyMember.RoleId.ToString().ToLower() == RolesConstant.Approver.ToLower()))
        {
            listOfUserId = _docToDataDBContext.CompanyMembers
                              .Where(m => m.CompanyId == companyMember.CompanyId)
                              .Select(m => m.UserId)
                              .ToList();
        }
        else
        {
            listOfUserId = new List<Guid> { userId };
        }

        var resultReceipt = _docToDataDBContext.Receipts
            .Where(m => listOfUserId.Contains(m.UserId))
            .Include(m => m.Merchant)
            .Include(m => m.Status)
             .Include(m => m.User)
             .Include(m => m.ApprovedByNavigation)
             .Include(m => m.ModifiedByNavigation)
            .AsNoTracking()
            .ToList();

        return resultReceipt.MapToReceiptHistory();
    }

}
