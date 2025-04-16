import { Component, inject, OnInit } from '@angular/core';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { ReceiptDashboardDTO } from '../interfaces/receipt-dashboard-dto';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { RouterLink} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-receipt-process-dashboard',
  imports: [DatePipe, CurrencyPipe, RouterLink, CommonModule],
  templateUrl: './receipt-process-dashboard.component.html',
  styleUrl: './receipt-process-dashboard.component.css'
})
export class ReceiptProcessDashboardComponent implements OnInit {
  receiptDetailsService = inject(ReceiptDetailsService);
  receiptDashboardDTO: ReceiptDashboardDTO[] = [];
  isLoading : boolean = true;
   roleName !: Observable<string>;
   authService = inject(AuthenticationService); 
  constructor() { }
  ngOnInit(): void {
    this.getFunctionAppReceiptDashboard();
    this.roleName = this.authService.getRoleNameDisplay;
  }

  getFunctionAppReceiptDashboard(){
    this.receiptDetailsService
            .getFunctionAppReceiptDashboard()
            .subscribe({
              next: (data : any) => {
                this.isLoading = true;
                this.receiptDashboardDTO = data.data.result
              },
              error: (error) => console.error(error),
              complete: () => this.isLoading = false
            }
          );
  }
}
