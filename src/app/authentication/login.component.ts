import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { UserValidateModeldto } from '../interfaces/user-validate-modeldto';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [RouterModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  authService = inject(AuthenticationService); 
  email: string = '';
  password: string = '';
  isLoading: boolean = false;
  userValidateModeldto : UserValidateModeldto = {
    email: '',
    password: ''
  }
     constructor(private router: Router){
     }

     onSubmit(){
      this.isLoading = true;
        this.userValidateModeldto.email = this.email;
        this.userValidateModeldto.password = this.password;
        this.authService
              .postFunctionAppUserValidate(this.userValidateModeldto)
              .subscribe({
                next: data => {
                  if(data.isSuccess === true){
                    this.authService.setIsLogged(true);
                    this.authService.setToken(data.data);
                    this.router.navigate(['/reports']);
                  } 
                  else{
                    alert(data.data);
                  }
                },
                error: (error) => console.log(error),
                complete: () => {this.isLoading = false;this.email = ""; this.password = "";}
             })
     }
}
