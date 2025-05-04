import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-forget-password',
  imports: [RouterModule, FormsModule],
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css'
})
export class ForgetPasswordComponent {
  email: string = '';
  loading: boolean = false;
  message: string = '';

  authenticationService = inject(AuthenticationService);

  constructor() { }

  onSubmit() {
    if (!this.email) return;
    this.authenticationService
            .getFunctionAppForgetPasswordLink(this.email)
            .subscribe({
              next: (data) =>{
                if(data.isSuccess == false){
                  alert(data.data)
                }
                else{
                  alert(data.data);
                }
              }
            })
  }
}
