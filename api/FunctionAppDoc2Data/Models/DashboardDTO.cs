﻿using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.Models;
public class DashboardDTO
{
    public IEnumerable<MerchantDashboardDTO> MerchantDashboardDTOs { get; set; }
    public IEnumerable<ExpenseMerchantDashboardDTO> ExpenseMerchantDashboardDTOs { get; set; }
    public decimal AvgMonSpending { get; set; }
    public decimal AvgDailySpending { get; set; }
    public int CurrentYear { get; set; }
    public decimal TotalSpendingTillToday { get; set; }
    public DateTime CurrentDate { get; private set; } = DateTime.UtcNow;
    public IEnumerable<MerchantMonthlyChartDTO> MerchantMonthlyCharts { get; set; }
    public IEnumerable<MerchantChartDTO> MerchantCharts { get; set; }
    public bool IsTotalSpendingTillTodayIncrease { get; set; }
    public bool IsAvgMonSpendingIncrease { get; set; }
    public bool IsAvgDailySpendingIncrease { get; set; }
}
