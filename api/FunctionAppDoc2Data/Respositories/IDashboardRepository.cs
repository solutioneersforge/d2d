using FunctionAppDoc2Data.Models;
using System;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IDashboardRepository
{
    Task<DashboardDTO> GetDashboardAsync(int parsedYear, int parsedMonth, Guid userId);
}