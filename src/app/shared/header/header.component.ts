import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';
import { CommonModule } from '@angular/common';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CompanyUpdateModalComponent } from '../../modal/company-update-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-header',
  imports: [RouterLink, RouterLinkActive, CommonModule, RouterModule,  ModalModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent  implements OnInit {
  isLoggedIn!: Observable<boolean>;
  userName !: Observable<string>;
  companyName !: Observable<string>;
  isAdminUser : boolean = false;
  roleName !: Observable<string>;
  companyEmail !: Observable<string>;
  companyAddress !: Observable<string>;
  companyPhoneNumber !: Observable<string>;
  authService = inject(AuthenticationService); 

  bsModalRef?: BsModalRef;
  
  /**
   *
   */
  constructor(private router : Router, private modalService: BsModalService) {
    
    
  }

  openModal() {
    this.bsModalRef = this.modalService.show(CompanyUpdateModalComponent, {
      class: 'modal-md'
    });
  }
  
  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn;
    this.userName = this.authService.getUserName;
    this.companyName = this.authService.getCompanyDisplay;
    this.roleName = this.authService.getRoleNameDisplay;
    this.isAdminUser = this.authService.getRoleName?.toLowerCase() == 'admin'; 
    this.companyAddress = this.authService.getCompanyAddress;
    this.companyEmail = this.authService.getCompanyEmail;
    this.companyPhoneNumber = this.authService.getCompanyPhoneNumber; 
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
    this.router.navigate(['/login']);
  }


}
