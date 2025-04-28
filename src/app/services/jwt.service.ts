import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class JwtService {

  constructor() { }

  getDecodedToken(token: string): any {
    try {
       if(token !== '')
          return jwtDecode(token);
        else
          return '';
    } catch (error) {
      console.error('Invalid Token', error);
      return null;
    }
  }

  getFullName(token: string): string {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken ? decodedToken.given_name || '' : '';
  }

  getEmail(token: string): string {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken ? decodedToken.unique_name || '' : '';
  }

  getUserId(token: string) : string {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken ? decodedToken.sub || '' : '';
  }

  getRoleName(token: string): string {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken ? decodedToken.role_name || '' : '';
  }

  getCompanyId(token: string): string {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken ? decodedToken.company_id || '' : '';
  }

  getCompanyName(token: string): string {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken ? decodedToken.company_name || '' : '';
  }

  getCompanyEmail(token: string): string {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken ? decodedToken.company_email || '' : '';
  }

  getCompanyPhoneNumber(token: string): string {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken ? decodedToken.company_phone || '' : '';
  }

  getCompanyAddress(token: string): string {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken ? decodedToken.company_address || '' : '';
  }
}

