using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.Mappers;
public static class DashboardMapper
{
    public static IEnumerable<MerchantDashboardDTO> MapToMerchantDashboard(this Receipt receipt)
    {
        if (receipt == null)
            throw new ArgumentNullException(nameof(MerchantDashboardDTO));

        return null;
    }
}
