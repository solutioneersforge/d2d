import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { ResetPasswordDTO } from '../interfaces/reset-password-dto';

@Component({
  selector: 'app-forget-password-link',
  imports: [RouterModule, FormsModule, CommonModule],

templateUrl: './forget-password-link.component.html',
  styleUrl: './forget-password-link.component.css'
})
export class ForgetPasswordLinkComponent implements OnInit {
  email: string = '';
  oldPassword: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  loading: boolean = false;
  message: string = '';
  forgetLinkToken: string = '';
  authenticationService = inject(AuthenticationService);
  resetPasswordDTO : ResetPasswordDTO = {
    forgetPasswordToken: '',
    resetPassword: ''
  };
  constructor(private route: ActivatedRoute, private router: Router) { }
  ngOnInit(): void {
    this.forgetLinkToken = this.route.snapshot.queryParamMap.get('forgetpasswordtoken') ?? '';
  }

  functionAppUpdateResetPassword(){
    this.resetPasswordDTO.forgetPasswordToken = this.forgetLinkToken;
    this.resetPasswordDTO.resetPassword = this.newPassword;
    this.authenticationService
        .postFunctionAppUpdateResetPassword(this.resetPasswordDTO)
        .subscribe({
          next: (data) => {
            if(data.isSuccess === false){
              alert(data.data);
            }
            else{
              alert(data.data);
              this.router.navigate(['/login']);
            }
          },
          error: (error) => console.log(error),
          complete: () => console.log('Completed')
        })
  }

  onSubmit() {
    this.functionAppUpdateResetPassword();
  }
}
