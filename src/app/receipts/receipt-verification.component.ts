import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { SafeResourceUrl } from '@angular/platform-browser';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { ExpenseCategoriesDTO } from '../interfaces/expense-categories-dto';
import { ReceiptVerificationMasterDTO } from '../interfaces/receipt-verification-master-dto';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReceiptApprovalDTO } from '../interfaces/receipt-approval-dto';
import { ReceiptItemsApprovalDTO } from '../interfaces/receipt-items-approval-dto';
import { Modal } from 'bootstrap';
import { UnitOfMeasuresDTO } from '../interfaces/unit-of-measures-dto';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-receipt-verification',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './receipt-verification.component.html',
  styleUrl: './receipt-verification.component.css'
})
export class ReceiptVerificationComponent implements OnInit {
  imageBase64: string = '';
  isImageLoad: boolean = true;
  isLoading = true;
  currentIndex: number = 0;
  receiptId: string = '';
  @ViewChild('myModal') modalElement!: ElementRef;
  modalInstance: Modal | null = null;
  unitOfMeasuresDTO: UnitOfMeasuresDTO[] = [];
  constructor(private activatedRoute: ActivatedRoute, private router: Router, private authenticationService: AuthenticationService){
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(data => {
      this.receiptId = data["id"];
      this.getFunctionAppReceiptVerification(data["id"]);
    });
  }

  getFunctionAppUnitOfMeasureDTO() {
    this.receiptDetailsService
      .getFunctionAppUnitOfMeasure()
      .subscribe({next: (data) => {
        this.unitOfMeasuresDTO = data.data;
      },
      error: (error) => console.log(error),
      complete : () => this.isLoading = false
    });
  }
  
  imageUrl: string | ArrayBuffer | null = null;
  expenseCategoriesDTO: ExpenseCategoriesDTO[] = [];
  receiptDetailsService = inject(ReceiptDetailsService);
  previewImage: string | ArrayBuffer | null = null;
  previewPdf: SafeResourceUrl | null = null;
  isSaveLoader: boolean = false;
  isSaveButtonEnable: boolean = false;
  receiptApprovalDTO!: ReceiptApprovalDTO;
  receiptItemsApprovalDTO: ReceiptItemsApprovalDTO[] = [];
  
  receiptVerificationMaster: ReceiptVerificationMasterDTO = {
    receiptId: '',
    userId: '',
    vendorName: '',
    vendorAddress: '',
    vendorPhone: '',
    vendorEmail: '',
    customerName: '',
    customerAddress: '',
    customerPhone: '',
    invoiceNumber: '',
    invoiceDate: '',
    subTotal: 0,
    taxAmount: 0,
    total: 0,
    imagePath: '',
    image : '',
    originalFileName: '',
    isImage: true,
    receiptVerificationItems: []
  };
  
  receiptFormGroup = new FormGroup({
    vendorName : new FormControl(this.receiptVerificationMaster.vendorName,[]),
    vendorAddress : new FormControl(this.receiptVerificationMaster.vendorAddress, []),
    vendorPhone: new FormControl(this.receiptVerificationMaster.vendorPhone, []),
    vendorEmail: new FormControl(this.receiptVerificationMaster.vendorEmail, []),
    invoiceNumber: new FormControl(this.receiptVerificationMaster.invoiceNumber, []),
    invoiceDate: new FormControl(this.receiptVerificationMaster.invoiceDate, []),
    customerName: new FormControl(this.receiptVerificationMaster.customerName, []),
    customerAddress: new FormControl(this.receiptVerificationMaster.customerAddress,[]),
    customerPhoneNumber : new FormControl(this.receiptVerificationMaster.customerPhone,[]),
    subTotal: new FormControl(this.receiptVerificationMaster.subTotal, []),
    taxAmount: new FormControl(this.receiptVerificationMaster.taxAmount,[]),
    total: new FormControl(this.receiptVerificationMaster.total,[]),
    imageBase64 : new FormControl(this.receiptVerificationMaster.image,[])
  });

  getExpenseSubCategoriesDTO(){
    this.receiptDetailsService.getExpenseSubCategoriesDTO().subscribe(data => {
      this.expenseCategoriesDTO = data.data;
    });
  }

  getFunctionAppReceiptVerification(receiptId: string){
          this.isLoading = true;
          this.receiptDetailsService.getFunctionAppReceiptVerification(receiptId).subscribe({ next: data => {
            console.log(data.data)
            this.receiptVerificationMaster = data.data;
            this.isImageLoad = this.receiptVerificationMaster.isImage;
            this.imageBase64 = this.receiptVerificationMaster.image;
            this.receiptFormGroup.setValue({
              vendorAddress : this.receiptVerificationMaster.vendorAddress,
              customerAddress: this.receiptVerificationMaster.customerAddress,
              customerName: this.receiptVerificationMaster.customerName,
              customerPhoneNumber: this.receiptVerificationMaster.customerPhone,
              invoiceDate: this.receiptVerificationMaster.invoiceDate,
              invoiceNumber: this.receiptVerificationMaster.invoiceNumber,
              subTotal: this.receiptVerificationMaster.subTotal,
              taxAmount: this.receiptVerificationMaster.taxAmount,
              total : this.receiptVerificationMaster.total,
              vendorEmail: this.receiptVerificationMaster.vendorEmail,
              vendorName: this.receiptVerificationMaster.vendorName,
              vendorPhone: this.receiptVerificationMaster.vendorPhone,
              imageBase64: this.receiptVerificationMaster.image,
            });
            this.receiptVerificationMaster.receiptVerificationItems = data.data.receiptVerificationItems;
          },
          error: (error) => console.error(error),
          complete: () => {
            this.isLoading = false; 
            this.getExpenseSubCategoriesDTO(); 
            this.getFunctionAppUnitOfMeasureDTO();
          }
          });
        }

  displayDataToFormControl(){
    
  }

  openApprovalConfirmation() {
    if (!this.modalInstance) {
      this.modalInstance = new Modal(this.modalElement.nativeElement);
    }
    this.modalInstance.show();
  }

  saveReceipt() {
   
     this.receiptApprovalDTO = {
        customerAddress: this.receiptFormGroup.value.customerAddress ?? '',
        customerName: this.receiptFormGroup.value.customerName ?? '',
        customerPhone: this.receiptFormGroup.value.customerPhoneNumber ?? '',
        approvedBy: this.authenticationService.getUserId,
        approvedOn: new Date().toISOString(),
        discount: 0,
        merchantAddress: this.receiptFormGroup.value.customerAddress ?? '',
        merchantEmail: this.receiptFormGroup.value.vendorEmail ?? '',
        merchantId: 0,
        merchantName: this.receiptFormGroup.value.vendorName ?? '',
        merchantPhone: this.receiptFormGroup.value.vendorPhone ?? '',
        otherCharge: 0,
        receiptDate: this.receiptFormGroup.value.invoiceDate ?? new Date().toISOString(),
        receiptId: this.receiptId,
        receiptNumber: this.receiptFormGroup.value.invoiceNumber ?? '',
        serviceCharge: 0,
        statusId: 1,
        subTotal: this.receiptFormGroup.value.subTotal ?? 0,
        taxAmount: this.receiptFormGroup.value.taxAmount ?? 0,
        totalAmount: this.receiptFormGroup.value.total ?? 0,
        userId: this.authenticationService.getUserId,
        receiptItemsApproval :  this.getItems()
     }

      this.receiptDetailsService
        .postFunctionAppReceiptApproval(this.receiptApprovalDTO)
        .subscribe(
          {
            next: data => {this.router.navigate(['/dashboard']);},
            error: (error) => console.log(error),
            complete: () => console.log('Data Successfully Completed')
         });
  }

  getItems(): ReceiptItemsApprovalDTO[]{
    this.receiptItemsApprovalDTO = [];
    this.receiptVerificationMaster.receiptVerificationItems.filter(
      item => item.itemDescription && item.itemDescription.trim() !== ''
    ).forEach(data => {
      this.receiptItemsApprovalDTO?.push({
        discount : data.discount,
        itemDescription : data.itemDescription,
        quantity : data.quantity,
        total : data.total,
        unitPrice : data.unitPrice,
        subCategoryId: data.subCategoryId ?? 0 ,
        receiptItemId: data.receiptItemID ?? this.generateGUID(),
        receiptId: this.receiptId,
        unitOfMeasureId : data.unitOfMeasureId
      })
  });

    return this.receiptItemsApprovalDTO;
  }

  generateGUID(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      const r = (Math.random() * 16) | 0,
            v = c === 'x' ? r : (r & 0x3) | 0x8;
      return v.toString(16);
    });
  }
  

  backToDashBoard(){
    this.router.navigate(['/dashboard']);
  }

  nextItem() {
    if (!(this.currentIndex < this.receiptVerificationMaster.receiptVerificationItems.length - 1)) {
      this.receiptVerificationMaster.receiptVerificationItems.push({
        discount: 0,
        itemDescription : '',
        quantity : 0,
        receiptItemID: '',
        subCategoryId : null,
        subCategoryName: '',
        total: 0,
        unitPrice: 0,
        unitOfMeasureId: 0
      });
    }
    this.currentIndex++;
    this.updateItemTotal(this.currentIndex);
  }

  previousItem(){
    this.currentIndex--;
    this.updateItemTotal(this.currentIndex);
  }

  get currentItem() {
    return this.receiptVerificationMaster.receiptVerificationItems[this.currentIndex] 
  }

  updateItemTotal(index: number) {
    const item = this.receiptVerificationMaster.receiptVerificationItems[index];
    item.total = (item.quantity  * item.unitPrice ) - (item.discount );
  }

  adjustItemInfo(){
    this.currentIndex = 0;
  }

  removeItem(){
    this.receiptVerificationMaster.receiptVerificationItems.splice(this.currentIndex, 1);
    this.nextItem();
  }
  
}


