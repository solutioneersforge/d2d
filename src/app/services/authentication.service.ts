import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { UserRegisterModeldto } from '../interfaces/user-register-modeldto';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserValidateModeldto } from '../interfaces/user-validate-modeldto';
import { JwtService } from './jwt.service';
import { UserModelDTO } from '../interfaces/user-model-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
 baseAddress : string = environment.apiUrl; 
 private loggedIn = new BehaviorSubject<boolean>(false);
 private token: string | null = null;
 private userName = new BehaviorSubject<string>("");
 private companyName = new BehaviorSubject<string>("");
 private roleName = new BehaviorSubject<string>("");

 private companyPhoneNumber = new BehaviorSubject<string>("");
 private companyEmail = new BehaviorSubject<string>("");
 private companyAddress = new BehaviorSubject<string>("");

constructor(private httpClient : HttpClient, private jwtService: JwtService) { }

get isLoggedIn() {
  return this.loggedIn.asObservable();
}

get getUserName() {
  return this.userName.asObservable();
}

get getCompanyDisplay() {
  return this.companyName.asObservable();
}


get getCompanyPhoneNumber(){
   return this.companyPhoneNumber.asObservable();
}

get getCompanyEmail(){
  return this.companyEmail.asObservable();
}

get getCompanyAddress(){
  return this.companyAddress.asObservable();
}

get getRoleNameDisplay(){
  return this.roleName.asObservable();
}

setIsLogged(isLoggedIn: boolean){
  this.loggedIn.next(isLoggedIn);
}

postFunctionAppRegistration(userRegisterModeldto: UserRegisterModeldto) : Observable<any>{
  return this.httpClient.post<any>(`${this.baseAddress}api/FunctionAppRegistration`, userRegisterModeldto);
}

postFunctionAppUpdateUser(userModeldto: UserModelDTO) : Observable<any>{
  return this.httpClient.post<any>(`${this.baseAddress}api/FunctionAppUpdateUser`, userModeldto);
}

postFunctionAppUserValidate(userValidateModeldto: UserValidateModeldto) : Observable<any>{
  return this.httpClient.post<any>(`${this.baseAddress}api/FunctionAppUserValidate`, userValidateModeldto);
}

setToken(token: string): void {
  sessionStorage.setItem('jwt_token', token);
  this.setUserName();
}

getToken(): string | null {
  return sessionStorage.getItem('jwt_token');
}

removeToken(){
  sessionStorage.removeItem('jwt_token');
  this.userName.next("");
  this.companyName.next("");
  this.loggedIn.next(false);
  this.roleName.next("");
  this.companyEmail.next("");
  this.companyAddress.next("");
  this.companyPhoneNumber.next("");
}

setUserName(){
  this.jwtService.getEmail(this.getToken() ?? "");
  this.userName.next(this.jwtService.getFullName(this.getToken() ?? ""));
  this.companyName.next(this.jwtService.getCompanyName(this.getToken() ?? ""));
  this.roleName.next(this.jwtService.getRoleName(this.getToken() ?? ""));
  this.companyEmail.next(this.jwtService.getCompanyEmail(this.getToken() ?? ""));
  this.companyAddress.next(this.jwtService.getCompanyAddress(this.getToken() ?? ""));
  this.companyPhoneNumber.next(this.jwtService.getCompanyPhoneNumber(this.getToken() ?? ""));
}

get getUserId() : string{
  return this.jwtService.getUserId(this.getToken() ?? "");
}

get getCompanyId(): string{
  return this.jwtService.getCompanyId(this.getToken() ?? "");
}

get getCompanyName(): string{
  return this.jwtService.getCompanyName(this.getToken() ?? "");
}

get getRoleName(): string{
  return this.jwtService.getRoleName(this.getToken() ?? "");
}

postFunctionAppUserVerification(verificationKey: string) : Observable<any>{
  return this.httpClient.post<any>(`${this.baseAddress}api/FunctionAppUserVerification?verificationKey=${verificationKey}`,null);
}

}
