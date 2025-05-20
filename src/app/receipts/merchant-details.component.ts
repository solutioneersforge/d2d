import { Component, inject, OnInit } from '@angular/core';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { MerchantDetailsDTO } from './../interfaces/merchant-details-dto';

@Component({
  selector: 'app-merchant-details',
  imports: [],
  
templateUrl: './merchant-details.component.html',
  styleUrl: './merchant-details.component.css'
})
export class MerchantDetailsComponent implements OnInit {
  totalRecords : number = 0;
  ngOnInit(): void {
    this.getMerchantDetailsDTO();
  }
receiptDetailsService = inject(ReceiptDetailsService);
merchantDetailsDTO: MerchantDetailsDTO[] =[];

   getMerchantDetailsDTO(){
        this.receiptDetailsService.getMerchantDetailsDTO().subscribe(data => {
          this.merchantDetailsDTO = data.data;
          this.totalRecords = this.merchantDetailsDTO.length;
        });
      }
}
