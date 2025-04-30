import { Component, inject, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-verification-user',
  imports: [],
  templateUrl: './verification-user.component.html',
  styleUrl: './verification-user.component.css'
})
export class VerificationUserComponent implements OnInit {
  authService = inject(AuthenticationService);
  responseMessage: string = '';
  isSuccessLoading: number = 1;
  constructor(private route: ActivatedRoute){
  
  }
ngOnInit(): void {
  this.route.queryParamMap.subscribe(params => {
    const verificationKey = params.get('verificationKey');
    if(verificationKey == null){

    }
    this.authService
          .postFunctionAppUserVerification(verificationKey ?? '')
          .subscribe({
            next: data => {
              if(data.isSuccess == true){
                this.isSuccessLoading = 2;
                
              }
              else{
                this.isSuccessLoading = 3;
              }
              this.responseMessage = data.data
            },
            error: (error) => this.isSuccessLoading = 3,
            complete: () => console.log('Completed')
          })
  });
}



}
