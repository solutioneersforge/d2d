import { Component, inject, Input, OnInit } from '@angular/core';
import { CompanyUpdateDTO } from '../interfaces/company-update-dto';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-company-update-modal',
  imports: [CommonModule, FormsModule],
  templateUrl: './company-update-modal.component.html',
  styleUrl: './company-update-modal.component.css'
})
export class CompanyUpdateModalComponent implements OnInit {
 
  authService = inject(AuthenticationService); 
  constructor(public bsModalRef: BsModalRef, private router: Router) {}
  ngOnInit(): void {
    this.authService.getCompanyDisplay.subscribe(data => this.companyData.companyName = data);
    this.authService.getCompanyAddress.subscribe(data => this.companyData.companyAddress = data);
    this.authService.getCompanyEmail.subscribe(data => this.companyData.companyEmail = data);
    this.authService.getCompanyPhoneNumber.subscribe(data => this.companyData.companyPhoneNumber = data); 
  }
   companyData: CompanyUpdateDTO = {
    companyName: '',
    companyEmail: '',
    companyPhoneNumber: '',
    companyAddress: ''
  };

  close() {
    this.bsModalRef.hide();
  }
  onSubmit(){
    console.log(this.companyData)
      this.authService
          .postFunctionAppUpdateCompany(this.companyData)
          .subscribe({
            next: (data) => {
              if(data.isSuccess === true){
                alert(data.data);
                this.authService.setIsLogged(true);
                this.authService.removeToken();
                this.router.navigate(['/login']);
              }
            },
            error: (error) => console.log(error),
            complete: () => this.bsModalRef.hide()
          })
  }
}
