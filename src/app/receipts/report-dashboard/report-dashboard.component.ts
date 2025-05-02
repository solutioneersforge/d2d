import { Component, inject, OnInit } from '@angular/core';
import { ReceiptDetailsService } from '../../services/receipt-details.service';
import {ExpenseMerchantDashboardDTO} from '../../interfaces/expense-merchant-dashboard-dto';
import { MerchantDashboardDTO } from '../../interfaces/merchant-dashboard-dto';
import { CommonModule } from '@angular/common';
import { BsDatepickerModule, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { FormsModule } from '@angular/forms';
import { jqxChartModule } from 'jqwidgets-ng/jqxchart';

@Component({
  selector: 'app-report-dashboard',
  imports: [CommonModule, BsDatepickerModule, FormsModule,jqxChartModule ],
  templateUrl: './report-dashboard.component.html',
  styleUrl: './report-dashboard.component.css'
})
export class ReportDashboardComponent implements OnInit {
  sampleDataMerchant: any;
  sampleDataMonthly : any;
  isLoading : boolean = false;

padding: any = { left: 20, top: 5, right: 20, bottom: 5 };
titlePadding: any = { left: 90, top: 0, right: 0, bottom: 10 };

xMonthlyAxis: any =
    {
        dataField: 'monthName',
        gridLines: { visible: false },
        flip: false,
        labels: {
          angle: -45, // optional: rotate to fit better
          step: 1     // force showing every label
      }
    };

xAxis: any =
    {
        dataField: 'merchantName',
        gridLines: { visible: false },
        flip: false,
        labels: {
          visible: false
        }
        
    };
getWidth(): any {
    if (document.body.offsetWidth < 850) {
        return '90%';
    }
    return 850;
}
valueAxis: any =
    {
        flip: false,
        labels: {
            visible: true,
            formatFunction: (value: string) => {
                return parseInt(value);
            }
        }
    };
seriesGroups: any[] =
    [
        {
            type: 'column',
            orientation: 'vertical',
            columnsGapPercent: 50,
            toolTipFormatSettings: { thousandsSeparator: ',' },
            series: [
                { dataField: 'total', displayText: 'Total Receipt Amount' }
            ]
        }
    ];

    seriesMonthlyGroups: any[] =
    [
        {
            type: 'column',
            orientation: 'vertical',
            columnsGapPercent: 50,
            toolTipFormatSettings: { thousandsSeparator: ',' },
            series: [
                { dataField: 'total', displayText: 'Total Receipt Amount' }
            ]
        }
    ];



  selectedDate: Date | null = null;
  currentYear: number = 0;
  avgMonthlySpending: number = 0;
  avgDailySpending: number = 0;
  totalSpendingTillToday: number = 0;
  currentDate : Date | undefined;

  selectedMonth: string = '';
  selectedYear: number = new Date().getFullYear();

  months = [
    { name: 'January', value: '01' },
    { name: 'February', value: '02' },
    { name: 'March', value: '03' },
    { name: 'April', value: '04' },
    { name: 'May', value: '05' },
    { name: 'June', value: '06' },
    { name: 'July', value: '07' },
    { name: 'August', value: '08' },
    { name: 'September', value: '09' },
    { name: 'October', value: '10' },
    { name: 'November', value: '11' },
    { name: 'December', value: '12' }
  ];

  years: number[] = [];
  constructor(){
    const currentDate = new Date();
    this.selectedMonth = (currentDate.getMonth() + 1).toString().padStart(2, '0');
    this.selectedYear = currentDate.getFullYear();

    const startYear = this.selectedYear - 50;
    const endYear = this.selectedYear + 10;
    for (let i = startYear; i <= endYear; i++) {
      this.years.push(i);
    }
  }

  ngOnInit(): void {
    this.getExpenseSubCategoriesDTO();
  }
  onOpenCalendar(container : any) {
    container.monthSelectHandler = (event: any): void => {
      container._store.dispatch(container._actions.select(event.date));
    };     
    container.setViewMode('month');
   }
  bsConfig: Partial<BsDatepickerConfig> = {
    minMode: 'month',
    dateInputFormat: 'MM/YYYY',
    containerClass: 'theme-dark-blue',
    showWeekNumbers: false,
    adaptivePosition: true
  };

  receiptDetailsService = inject(ReceiptDetailsService);
  expenseSubCategoriesDTO: ExpenseMerchantDashboardDTO[] = [];
  merchantDashboardDTO: MerchantDashboardDTO[] = [];

  getExpenseSubCategoriesDTO() {
    this.receiptDetailsService
      .getFunctionAppDashboardReport(this.selectedYear, this.selectedMonth)
      .subscribe({next: (data) => {
        this.merchantDashboardDTO = data.data.merchantDashboardDTOs;
        this.expenseSubCategoriesDTO = data.data.expenseMerchantDashboardDTOs;
        this.currentYear = data.data.currentYear;
        this.avgMonthlySpending = data.data.avgMonSpending;
        this.avgDailySpending = data.data.avgDailySpending;
        this.totalSpendingTillToday = data.data.totalSpendingTillToday;
        this.currentDate = data.data.currentDate;
        this.sampleDataMerchant = data.data.merchantCharts;
        this.sampleDataMonthly = data.data.merchantMonthlyCharts;
      },
      error: (error) => console.log(error),
      complete : () => this.isLoading = true
    });
  }
}
