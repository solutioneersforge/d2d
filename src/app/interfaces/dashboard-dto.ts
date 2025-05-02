import { ExpenseMerchantDashboardDTO } from "./expense-merchant-dashboard-dto";
import { MerchantDashboardDTO } from "./merchant-dashboard-dto";
import { MerchantMonthlyChartDTO } from "./merchant-monthly-chart-dto";

export interface DashboardDTO {
    expenseMerchantDashboardDTO: ExpenseMerchantDashboardDTO[] 
    merchantDashboardDTO: MerchantDashboardDTO[] ;
    merchantMonthlyCharts: MerchantMonthlyChartDTO[];
}
