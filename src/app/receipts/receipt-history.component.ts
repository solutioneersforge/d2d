import { AfterViewInit, Component, ElementRef, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { ReceiptHistoryDTO } from '../interfaces/receipt-history-dto';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerConfig, BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RejectReceiptDTO } from '../interfaces/reject-receipt-dto';

declare var bootstrap: any;

@Component({
  selector: 'app-receipt-history',
  imports: [DatePipe, CurrencyPipe, CommonModule, BsDropdownModule, BsDatepickerModule, FormsModule ],
  templateUrl: './receipt-history.component.html',
  styleUrl: './receipt-history.component.css'
})
export class ReceiptHistoryComponent implements OnInit, AfterViewInit  {
  constructor(private router: Router){
    
  }

  alert: {
  type: string;
  message: string;
  visible: boolean;
} = {
  type: '',
  message: '',
  visible: false
};


  closeAlert() {
     this.alert.visible = false;
  }

  receiptId: string = '';

  confirmAction(){
    const modalEl = document.getElementById('confirmModal');
    const modalInstance = bootstrap.Modal.getInstance(modalEl);
    modalInstance?.hide();

     this.rejectReceiptDTO = {
      rejectComment: this.rejectComment,
       receiptId : this.receiptId
    };
     this.postFunctionAppRejectReceipt(this.rejectReceiptDTO);
  }
  receiptDetailsService = inject(ReceiptDetailsService);
  receiptDashboardDTO: ReceiptHistoryDTO[] = [];
  rejectComment: string = '';
  rejectReceiptDTO: RejectReceiptDTO = {
    receiptId: '',
    rejectComment: '',
  };
  selectedMonth = 0;
  monthOptions = [
     { label: '0 Month', value: 0 },
  { label: '1 Month', value: 1 },
  { label: '2 Month', value: 2 },
  { label: '3 Month', value: 3 },
  { label: '6 Month', value: 6 },
  { label: '12 Month', value: 12 }
];
  selectedOption: string = ''; // Track selected option
  isLoading: boolean = true;
  selectedDate: Date = new Date();
  fromDate: Date = new Date();
  toDate : Date = new Date();
  maxDate: Date = new Date();;
   bsConfig: Partial<BsDatepickerConfig> = {  
     dateInputFormat: 'YYYY-MM-DD',
    containerClass: 'theme-dark-blue',
    isAnimated: true,
    showWeekNumbers: false,
    showTodayButton: true,
    adaptivePosition: true,  // For mobile responsiveness
    maxDate: new Date()      // Optional: restrict future dates
  };

  onMonthSelect(selectedValue: number){
    this.selectedMonth = selectedValue;
    this.calculateFromDate();
  }

   private calculateFromDate(): void {
    const newFromDate = new Date(this.toDate);
    newFromDate.setMonth(this.toDate.getMonth() - this.selectedMonth);
    this.fromDate = newFromDate;
  }


  selectedRange: any = {
    start: new Date(),
    end: new Date(new Date().setDate(new Date().getDate() + 7))
  };
  
  ngOnInit(): void {
    

     this.selectedDate = new Date();
  
    const savedDates = localStorage.getItem('dateFilters');
  if (savedDates) {
    const dates = JSON.parse(savedDates);
    this.fromDate = new Date(dates.fromDate);
    this.toDate = new Date(dates.toDate);
  }
  this.getFunctionAppReceiptHistory();
  }

  onSearch(){
    this.getFunctionAppReceiptHistory();
    this.saveDates();
  }

  saveDates() {
  localStorage.setItem('dateFilters', JSON.stringify({
    fromDate: this.fromDate,
    toDate: this.toDate
  }));
}

  getFunctionAppReceiptHistory() {
    this.receiptDetailsService
      .getFunctionAppReceiptHistory(this.fromDate, this.toDate)
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


   // Method to handle menu item clicks
  onMenuItemClick(action: string, item: any) {
    this.selectedOption = action;
    
    // You can add specific logic for each action
    switch(action) {
      case 'Edit':
        this.onEdit(item.receiptId);
        break;
      case 'View':
        this.onView();
        break;
      case 'Approve':
        this.onApprove(item.receiptId);
        break;
      case 'Reject':
        this.onReject(item.receiptId);
        break;
      case 'Download PDF':
        this.onDownloadPDF();
        break;
      default:
        console.log('Unknown action');
    }
  }

  // Individual methods for each action
  onEdit(receiptId: any) {
      this.router.navigate(['/receiptModification', receiptId], {
    queryParams: { status: 'edit' }
  });
  }

  onView() {
    console.log('View clicked');
    // Add your view logic here
  }

  onApprove(receiptId: any) {
     this.router.navigate(['/receiptverification', receiptId], {
    queryParams: { status: 'approved' }
  });
  }
modalInstance: any;
 @ViewChild('confirmModal', { static: false }) confirmModalRef!: ElementRef;
  onReject(receiptId: any) {
    this.receiptId = receiptId
    this.modalInstance.show();
   
  }

  ngAfterViewInit(): void {
    // Initialize the modal
    this.modalInstance = new bootstrap.Modal(this.confirmModalRef.nativeElement);
  }

  onDownloadPDF() {
    console.log('Download PDF clicked');
    // Add your PDF download logic here
  }

 showToast(type: string, message: string) {
  this.alert.type = type;
  this.alert.message = message;
  this.alert.visible = true;

  setTimeout(() => {
    this.alert.visible = false;
  }, 4000); // auto dismiss
}

closeToast() {
  this.alert.visible = false;
}

  postFunctionAppRejectReceipt(rejectReceiptDTO: RejectReceiptDTO){
   
    this.receiptDetailsService
          .postFunctionAppRejectReceipt(rejectReceiptDTO)
          .subscribe({ next: data => {
             debugger;
            if(data.isSuccess){
                this.getFunctionAppReceiptHistory();
                this.showToast('success', data.message)
            }
            else{
               this.showToast('danger', data.message)
            }
          },
          error: (data) => console.log(data),
          complete: () => console.log('Completed')
        })
  }

}

