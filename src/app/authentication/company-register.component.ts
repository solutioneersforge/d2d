import { Component, inject } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router, RouterModule } from '@angular/router';
import { UserRegisterModeldto } from '../interfaces/user-register-modeldto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-company-register',
  imports: [RouterModule, CommonModule, FormsModule],
  templateUrl: './company-register.component.html',
  styleUrl: './company-register.component.css',
})
export class CompanyRegisterComponent {
  authService = inject(AuthenticationService);
  constructor(private router: Router) {}
  isLoading:boolean = false;
  companyName: string = '';
  firstName: string = '';
  lastName: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  captchaInput: string = '';
  address: string = '';
  telephoneNumber : string = '';
  companyEmail: string = '';
  
  userRegisterModeldto: UserRegisterModeldto = {
    companyName: '',
    firstName: '',
    lastName: '',
    email: '',
    password: '',
     companyId : '',
     roleId: '',
     telephoneNumber: '',
     address: '',
     companyEmail: '',
  };
  captchaAnswer: number = 8;

  onSubmit() {
    if (this.password !== this.confirmPassword) {
      alert('Passwords do not match!');
      return;
    }
    // if (parseInt(this.captchaInput) !== this.captchaAnswer) {
    //   alert("Captcha is incorrect!");
    //   return;
    // }
    this.userRegisterModeldto.companyName = this.companyName;
    this.userRegisterModeldto.email = this.email;
    this.userRegisterModeldto.firstName = this.firstName;
    this.userRegisterModeldto.lastName = this.lastName;
    this.userRegisterModeldto.password = this.password;
    this.userRegisterModeldto.telephoneNumber = this.telephoneNumber;
    this.userRegisterModeldto.address = this.address;
    this.userRegisterModeldto.companyEmail = this.companyEmail;
    this.isLoading = true;
    this.authService
      .postFunctionAppRegistration(this.userRegisterModeldto)
      .subscribe({
        next: (data) => {
          if (data.isSuccess === true) {
            alert(data.data);
            this.router.navigate(['/login']);
          } else {
            alert(data.data);
          }
        },
        error: (error) => console.log(error),
        complete: () => this.isLoading = false
      });
  }
}
