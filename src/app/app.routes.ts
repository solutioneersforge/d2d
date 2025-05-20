import { Routes } from '@angular/router';
import { HomeComponent } from './shared/header/home.component';
import { ContactComponent } from './shared/header/contact.component';
import { AboutComponent } from './shared/header/about.component';
import { ReceiptProcessComponent } from './receipts/receipt-process/receipt-process.component';
import { PagenotfoundComponent } from './shared/header/pagenotfound/pagenotfound.component';
import { LoginComponent } from './authentication/login.component';
import { RegistartionComponent } from './authentication/registartion.component';
import { ReceiptProcessDashboardComponent } from './receipts/receipt-process-dashboard.component';
import { ReceiptVerificationComponent } from './receipts/receipt-verification.component';
import { ReceiptModificationComponent } from './receipts/receipt-modification.component';
import { ForgetPasswordComponent } from './authentication/forget-password.component';
import { ReceiptHistoryComponent } from './receipts/receipt-history.component';
import { ExpenseTypeComponent } from './receipts/expense-type.component';
import { ReportDashboardComponent } from './receipts/report-dashboard/report-dashboard.component';
import { AuthGuardService } from './services/auth-guard.service';
import { RegisterTypeComponent } from './authentication/register-type.component';
import { CompanyRegisterComponent } from './authentication/company-register.component';
import { CompanyUserRegistrationComponent } from './authentication/company-user-registration.component';
import { VerificationUserComponent } from './authentication/verification-user.component';
import { ForgetPasswordLinkComponent } from './authentication/forget-password-link.component';
import { MerchantDetailsComponent } from './receipts/merchant-details.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'about', component: AboutComponent },
  { path: 'scan', component: ReceiptProcessComponent },
  { path: 'resetpassword', component: ForgetPasswordLinkComponent },
  { path: 'dashboard', component: ReceiptProcessDashboardComponent, canActivate: [AuthGuardService]  },
  { path: 'receiptverification/:id', component: ReceiptVerificationComponent, canActivate: [AuthGuardService] },
  { path: 'receiptModification/:id', component: ReceiptModificationComponent, canActivate: [AuthGuardService] },
  { path: 'login', component: LoginComponent },
  { path: 'registertype', component: RegisterTypeComponent },
  { path: 'individualregister', component: RegistartionComponent },
  { path: 'companyregister', component: CompanyRegisterComponent },
  { path: 'forgetpassword', component: ForgetPasswordComponent },
  {path: 'verificationuser', component: VerificationUserComponent},
  { path: 'history', component: ReceiptHistoryComponent, canActivate: [AuthGuardService] },
  { path: 'accounts', component: ExpenseTypeComponent, canActivate: [AuthGuardService] },
  { path: 'reports', component: ReportDashboardComponent, canActivate: [AuthGuardService]},
  { path: 'users', component: CompanyUserRegistrationComponent, canActivate: [AuthGuardService]},
  { path: 'merchant', component: MerchantDetailsComponent, canActivate: [AuthGuardService]},
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', component: PagenotfoundComponent },
];
