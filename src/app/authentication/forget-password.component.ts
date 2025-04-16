import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-forget-password',
  imports: [RouterModule, FormsModule],
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css'
})
export class ForgetPasswordComponent {
  email: string = '';
  oldPassword: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  loading: boolean = false;
  message: string = '';

  constructor() { }

  onSubmit() {
    if (!this.email) return;

    this.loading = true;
    this.message = '';

    setTimeout(() => {
      this.loading = false;
      this.message = "If this email exists, you'll receive a password reset link.";
    }, 2000);
  }
}
