import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [RouterModule,CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  title = 'Receipt Scanner App';
isLoggedIn!: Observable<boolean>;
authService = inject(AuthenticationService); 

  constructor(private router: Router){
    
  }
  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn;
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      console.log('File selected:', file.name);
    }
  }

  goToDashboard() {
    this.router.navigate(['/dashboard']);
  }

  goToUsers() {
    this.router.navigate(['/users']);
  }

  goToScan() {
    this.router.navigate(['/scan']);
  }

  goToHistory() {
    this.router.navigate(['/history']);
  }

  goToAccounts() {
    this.router.navigate(['/accounts']);
  }

  goToReports() {
    this.router.navigate(['/reports']);
  }
}
