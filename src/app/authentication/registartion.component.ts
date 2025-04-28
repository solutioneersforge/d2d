import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserRegisterModeldto } from '../interfaces/user-register-modeldto';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-registartion',
  imports: [FormsModule, CommonModule],
  templateUrl: './registartion.component.html',
  styleUrl: './registartion.component.css'
})
export class RegistartionComponent {
  authService = inject(AuthenticationService);
   constructor(private router: Router){
    
   }
  firstName: string = '';
  lastName: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  captchaInput: string = '';
  isLoading : boolean = false;
  userRegisterModeldto: UserRegisterModeldto = {
    companyName: '',
    firstName: '',
    lastName: '',
    email: '',
    password: '',
     companyId : '',
     roleId: '',
     address: '',
     telephoneNumber: '',
     companyEmail: ''
  };
  captchaAnswer: number = 8;

  onSubmit() {
    if (this.password !== this.confirmPassword) {
      alert("Passwords do not match!");
      return;
    }
    

    this.userRegisterModeldto.email = this.email;
    this.userRegisterModeldto.firstName = this.firstName;
    this.userRegisterModeldto.lastName = this.lastName;
    this.userRegisterModeldto.password = this.password;
    this.isLoading = true;
    this.authService
          .postFunctionAppRegistration(this.userRegisterModeldto)
          .subscribe({
            next: data => {
              if(data.isSuccess === true){
                alert(data.data);
                this.router.navigate(['/login']);
              } 
              else{
                alert(data.data);
              }
            },
            error: (error) => console.log(error),
            complete: () => this.isLoading = false
         });
  }
}
