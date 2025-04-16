import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  imports: [RouterLink, RouterLinkActive, CommonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent  implements OnInit {
  isLoggedIn!: Observable<boolean>;
  userName !: Observable<string>;
  companyName !: Observable<string>;
  isAdminUser : boolean = false;
  roleName !: Observable<string>;

  authService = inject(AuthenticationService); 

  /**
   *
   */
  constructor(private router : Router) {
    
    
  }
  
  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn;
    this.userName = this.authService.getUserName;
    this.companyName = this.authService.getCompanyDisplay;
    this.roleName = this.authService.getRoleNameDisplay;
    this.isAdminUser = this.authService.getRoleName?.toLowerCase() == 'admin'; 

  }

  collapseNavbar() {
    const navbarCollapse = document.getElementById('navbarNav');
    if (navbarCollapse) {
      navbarCollapse.classList.remove('show');
    }
  }

  collapseNavbarLogout(){
    const navbarCollapse = document.getElementById('navbarNav');
    if (navbarCollapse) {
      navbarCollapse.classList.remove('show');
    }
    this.authService.setIsLogged(true);
      this.authService.removeToken();
     
  }


}
