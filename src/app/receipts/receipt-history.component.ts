import { Component, inject, OnInit } from '@angular/core';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { ReceiptHistoryDTO } from '../interfaces/receipt-history-dto';

@Component({
  selector: 'app-receipt-history',
  imports: [DatePipe, CurrencyPipe, CommonModule ],
  templateUrl: './receipt-history.component.html',
  styleUrl: './receipt-history.component.css'
})
export class ReceiptHistoryComponent implements OnInit {
  receiptDetailsService = inject(ReceiptDetailsService);
  receiptDashboardDTO: ReceiptHistoryDTO[] = [];
  isLoading: boolean = true;
  constructor() { }
  ngOnInit(): void {
    this.getFunctionAppReceiptHistory();
  }

  getFunctionAppReceiptHistory() {
    this.receiptDetailsService
      .getFunctionAppReceiptHistory()
      .subscribe({
        next: (data: any) => {
          this.isLoading = true;
          this.receiptDashboardDTO = data.data
        },
        error: (error) => console.error(error),
        complete: () => this.isLoading = false
      }
      );
  }

  getStatusClass(status: string): string {
    switch (status.toLowerCase()) {
      case '1':
        return 'text-danger'; // Red for inactive
      case '2':
        return 'text-success'; // Green for active
      case '3':
         return 'text-warning'; // Yellow for pending
      default:
        return 'text-secondary'; // Default gray
    }
  }
}

