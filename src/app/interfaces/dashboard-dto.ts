import { ExpenseMerchantDashboardDTO } from "./expense-merchant-dashboard-dto";
import { MerchantDashboardDTO } from "./merchant-dashboard-dto";

export interface DashboardDTO {
    expenseMerchantDashboardDTO: ExpenseMerchantDashboardDTO[] 
    merchantDashboardDTO: MerchantDashboardDTO[] ;
}
