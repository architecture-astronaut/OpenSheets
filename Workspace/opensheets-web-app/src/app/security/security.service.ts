import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { LoginModel } from '../models/login-model';
import { RegisterModel } from '../models/register-model';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  private httpOptions = {};

  private securityUrl = 'api/security/';

  constructor(private http: HttpClient) { }

  login(model: LoginModel) : Observable<string[]> {
      return this.http.post<string[]>(this.securityUrl + 'login', model);
  }

  register(model: RegisterModel) : void {
      this.http.post(this.securityUrl + 'register', model);
  }

  forgot(email: string) : void {
      this.http.post(this.securityUrl + 'forgot', email);
  }

  reset(token: string, password: string) : Observable<string[]> {
      return this.http.post<string[]>(this.securityUrl + 'reset?token=' + token, password);
  }
}
