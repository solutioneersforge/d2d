import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { UserRegisterModeldto } from '../interfaces/user-register-modeldto';
import { AuthenticationService } from '../services/authentication.service';
import { JwtService } from '../services/jwt.service';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { RolesDTO } from '../interfaces/roles-dto';
import { CompanyUserDTO } from '../interfaces/company-user-dto';
import { UserModelDTO } from '../interfaces/user-model-dto';

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
  updatedUserId: string = '';
  userModelDTO: UserModelDTO = {
    userId: '',
    firstName: '',
    lastName: '',
    roleId: '',
    isActive: false
  };

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
      companyEmail:'',
      currencyId : 0,
      isTrackInventory : false
    };

  constructor(private fb: FormBuilder) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      isActive: [true, [Validators.required]],
      roleId: ['', Validators.required]
    }, {
      validator: this.passwordMatchValidator
    });
  }
  ngOnInit(): void {
    this.getRoles();
    this.getFunctionAppGetCompanyUser();
  }

  resetUserForm(){
    this.userForm.reset();
    this.isEdit = false;
    this.updatedUserId = "";
    this.addEmailAndPasswordValidators();
  }

  passwordMatchValidator(group: AbstractControl): ValidationErrors | null {
    const password = group.get('password');
    const confirmPassword = group.get('confirmPassword');
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      return { passwordMismatch: true };
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

    if(this.isEdit){this.isLoading = true;
      this.userModelDTO = {
        firstName:  this.userForm.get('firstName')?.value || '',
        isActive: this.userForm.get('isActive')?.value,
        lastName :  this.userForm.get('lastName')?.value || '',
        roleId :  this.userForm.get('roleId')?.value || '',
        userId:  this.updatedUserId,
      };
      console.log(this.userModelDTO);
      this.authService
        .postFunctionAppUpdateUser(this.userModelDTO)
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
          complete: () => {this.isLoading = false; this.resetUserForm();}
        });
    }

    else{

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
      companyEmail: this.userForm.get('companyEmail')?.value || '',
      currencyId:0,
      isTrackInventory : false
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
      complete: () => {this.isLoading = false; this.resetUserForm();}
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
}

  editUser(user: any, index: number) {
    this.userForm.patchValue({
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      roleId: user.roleId,
      isActive: user.isActive
    });
    debugger;
    this.isEdit = true;
    this.editIndex = index;
    this.updatedUserId = user.userId;
    this.removeEmailAndPasswordValidators();
  }

  deleteUser(index: number) {
    this.users.splice(index, 1);
  }

  removeEmailAndPasswordValidators() {
    const emailControl = this.userForm.get('email');
    const passwordControl = this.userForm.get('password');
    const confirmPasswordControl = this.userForm.get('confirmPassword');
  
    emailControl?.clearValidators();
    passwordControl?.clearValidators();
    confirmPasswordControl?.clearValidators();
  
    emailControl?.updateValueAndValidity();
    passwordControl?.updateValueAndValidity();
    confirmPasswordControl?.updateValueAndValidity();
  
    // Optional: If you have a custom form-level validator like `passwordMatchValidator`,
    // and you want to disable that too, remove or reassign it:
    this.userForm.setValidators(null);
    this.userForm.updateValueAndValidity();
  }

  addEmailAndPasswordValidators() {
    this.userForm.get('email')?.setValidators([Validators.required, Validators.email]);
    this.userForm.get('password')?.setValidators([Validators.required, Validators.minLength(6)]);
    this.userForm.get('confirmPassword')?.setValidators([Validators.required]);
  
    this.userForm.get('email')?.updateValueAndValidity();
    this.userForm.get('password')?.updateValueAndValidity();
    this.userForm.get('confirmPassword')?.updateValueAndValidity();
    this.userForm.setValidators(this.passwordMatchValidator.bind(this));
    this.userForm.updateValueAndValidity();
  }
  
  
}
