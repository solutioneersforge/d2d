import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserRegisterModeldto } from '../interfaces/user-register-modeldto';
import { AuthenticationService } from '../services/authentication.service';
import { JwtService } from '../services/jwt.service';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { RolesDTO } from '../interfaces/roles-dto';
import { CompanyUserDTO } from '../interfaces/company-user-dto';

@Component({
  selector: 'app-company-user-registration',
  imports: [FormsModule, CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './company-user-registration.component.html',
  styleUrl: './company-user-registration.component.css'
})
export class CompanyUserRegistrationComponent implements OnInit {
  userForm: FormGroup;
  users: any[] = [];
  isEdit : boolean = false;
  editIndex: number | null = null;
  authService = inject(AuthenticationService);
  isLoading: boolean = false;
  receiptDetailsService = inject(ReceiptDetailsService);
  rolesDTO: RolesDTO[] = [];
  companyUserDTO: CompanyUserDTO[] = [];

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
    };

  constructor(private fb: FormBuilder) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      roleId: ['', Validators.required]
    }, {
      validator: this.passwordMatchValidator
    });
  }
  ngOnInit(): void {
    this.getRoles();
    this.getFunctionAppGetCompanyUser();
  }

  passwordMatchValidator(group: FormGroup): { [key: string]: boolean } | null {
    const password = group.get('password');
    const confirmPassword = group.get('confirmPassword');
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      return { 'passwordMismatch': true };
    }
    return null;
  }

  hasError(controlName: string): boolean {
    const control = this.userForm.get(controlName);
    return !!(control && control.invalid && (control.dirty || control.touched));
}

getFunctionAppGetCompanyUser(){
  this.receiptDetailsService
          .getFunctionAppGetCompanyUser()
          .subscribe({
            next: (data) => {
              this.companyUserDTO = data.data
            },
            error: (error) => console.log(error),
            complete: () => console.log('Completed')
          })
}

 getRoles(){
  this.receiptDetailsService
          .getFunctionAppRoles()
          .subscribe({
            next: (data) => {
              this.rolesDTO = data.data
            },
            error: (error) => console.log(error),
            complete: () => console.log('Completed')
          });
 }

  onSubmit() {
    if (this.userForm.invalid) {
      return;
    }

    this.userRegisterModeldto = {
      companyName : '',
      email : this.userForm.get('email')?.value || '',
      firstName: this.userForm.get('firstName')?.value || '',
      lastName : this.userForm.get('lastName')?.value || '',
      password:  this.userForm.get('password')?.value || '',
      companyId: this.authService.getCompanyId,
      roleId:  this.userForm.get('roleId')?.value || '',
      telephoneNumber: this.userForm.get('telephoneNumber')?.value || '',
      address: this.userForm.get('address')?.value || '',
    }
    this.isLoading = true;
    this.authService
    .postFunctionAppRegistration(this.userRegisterModeldto)
    .subscribe({
      next: (data) => {
        if (data.isSuccess === true) {
          alert(data.data);
          this.userForm.reset();
          this.getFunctionAppGetCompanyUser();
        } else {
          alert(data.data);
        }
      },
      error: (error) => console.log(error),
      complete: () => this.isLoading = false
    });

    if (this.isEdit && this.editIndex !== null) {
      this.users[this.editIndex] = this.userForm.value;
      this.isEdit = false;
      this.editIndex = null;
    } else {
      this.users.push(this.userForm.value);
    }

    this.userForm.reset();
  }

  editUser(user: any, index: number) {
    this.userForm.setValue(user);
    this.isEdit = true;
    this.editIndex = index;
  }

  deleteUser(index: number) {
    this.users.splice(index, 1);
  }
}
