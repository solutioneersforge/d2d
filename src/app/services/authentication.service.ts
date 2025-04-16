import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { UserRegisterModeldto } from '../interfaces/user-register-modeldto';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserValidateModeldto } from '../interfaces/user-validate-modeldto';
import { JwtService } from './jwt.service';

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

get getRoleNameDisplay(){
  return this.roleName.asObservable();
}

setIsLogged(isLoggedIn: boolean){
  this.loggedIn.next(isLoggedIn);
}

postFunctionAppRegistration(userRegisterModeldto: UserRegisterModeldto) : Observable<any>{
  return this.httpClient.post<any>(`${this.baseAddress}api/FunctionAppRegistration`, userRegisterModeldto);
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
  this.userName.next(this.jwtService.getFullName(this.getToken() ?? ""));
  this.companyName.next(this.jwtService.getCompanyName(this.getToken() ?? ""));
  this.loggedIn.next(false);
}

setUserName(){
  this.jwtService.getEmail(this.getToken() ?? "");
  this.userName.next(this.jwtService.getFullName(this.getToken() ?? ""));
  this.companyName.next(this.jwtService.getCompanyName(this.getToken() ?? ""));
  this.roleName.next(this.jwtService.getRoleName(this.getToken() ?? ""));
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

}
