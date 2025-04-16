import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { ReceiptDetails } from '../interfaces/receipt-details.model';
import { Observable } from 'rxjs';
import { ReceiptMasterDTO } from '../interfaces/receipt-master-dto';
import { ReceiptApprovalDTO } from '../interfaces/receipt-approval-dto';
import { ExpenseTypeDTO } from '../interfaces/expense-type-dto';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class ReceiptDetailsService {
  baseAddress : string = environment.apiUrl; 
  formData = new FormData();
  constructor(private httpClient : HttpClient, private authenticationService: AuthenticationService ) { }
  getReceiptItemsDetails(file: any): Observable<any>{
    const formData = new FormData();
    formData.append('file', file);
    return this.httpClient.post<any>(`${this.baseAddress}api/FunctionDoc2Data`, formData);
  }

  getExpenseSubCategoriesDTO() : Observable<any>{
    return this.httpClient.get<any>(`${this.baseAddress}api/FunctionAppCategoryExpenseType`);
  }


  postAppCreateReceipt(receiptMasterDTO: ReceiptMasterDTO, file: any) : Observable<any>{
    const formData = new FormData();
    formData.append('file', file);
    formData.append('receiptMasterDTO', JSON.stringify(receiptMasterDTO));
    return this.httpClient.post<any>(`${this.baseAddress}api/FunctionAppCreateReceipt`,formData);
  }

  getFunctionAppReceiptDashboard() : Observable<any>{
    return this.httpClient.get<any>(`${this.baseAddress}api/FunctionAppReceiptDashboard`);
  }

  getFunctionAppReceiptVerification(receiptId: string) : Observable<any>{
    return this.httpClient.get<any>(`${this.baseAddress}api/FunctionAppReceiptVerification?receiptId=`+ receiptId);
  }

  postFunctionAppReceiptApproval(receiptApprovalDTO: ReceiptApprovalDTO) : Observable<any>{
    return this.httpClient.post<any>(`${this.baseAddress}api/FunctionAppReceiptApproval`, receiptApprovalDTO);
  }
  
  postFunctionAppReceiptModification(receiptApprovalDTO: ReceiptApprovalDTO) : Observable<any>{
    return this.httpClient.post<any>(`${this.baseAddress}api/FunctionAppReceiptModification`, receiptApprovalDTO);
  }

  getFunctionAppReceiptHistory(): Observable<any> {
    return this.httpClient.get<any>(`${this.baseAddress}api/FunctionAppReceiptHistory`);
  }

  postFunctionAppExpenseType(expenseTypeDTO: ExpenseTypeDTO) : Observable<any>{
    return this.httpClient.post<any>(`${this.baseAddress}api/FunctionAppExpenseType`, expenseTypeDTO);
  }
  
  getFunctionAppUnitOfMeasure(): Observable<any> {
    return this.httpClient.get<any>(`${this.baseAddress}api/FunctionAppUnitOfMeasure`);
  }

  getFunctionAppDashboardReport(year: any, month: any): Observable<any> {
    return this.httpClient.get<any>(`${this.baseAddress}api/FunctionAppDashboardReport?year=${year}&month=${month}`);
  }

  getFunctionAppRoles(): Observable<any> {
    return this.httpClient.get<any>(`${this.baseAddress}api/FunctionAppRoles`);
  }

  getFunctionAppGetCompanyUser(): Observable<any> {
    return this.httpClient.get<any>(`${this.baseAddress}api/FunctionAppGetCompanyUser`);
  }
}
