import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-register-type',
  imports: [RouterModule, FormsModule],
  templateUrl: './register-type.component.html',
  styleUrl: './register-type.component.css'
})
export class RegisterTypeComponent {

 constructor(private router: Router){}

  individualRegister(){
      this.router.navigate(['/individualregister']);
  }

  companyRegister(){
    this.router.navigate(['/companyregister']);
  }
}
