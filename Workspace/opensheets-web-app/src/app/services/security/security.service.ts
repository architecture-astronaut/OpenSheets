import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { token, loginResponse } from 'src/app/models/token';
import { LoginModel } from 'src/app/models/login-model';
import { RegisterModel } from 'src/app/models/register-model';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  private httpOptions = {};

  private securityUrl = 'api/security/';

  constructor(private http: HttpClient) { }

  login(model: LoginModel) : Observable<loginResponse> {
      return this.http.post<loginResponse>(this.securityUrl + 'login', model);
  }

  register(model: RegisterModel) : void {
      this.http.post(this.securityUrl + 'register', model);
  }

  refresh() : void {
	  this.http.get(this.securityUrl + 'refresh')
  }

  forgot(email: string) : void {
      this.http.post(this.securityUrl + 'forgot', email);
  }

  reset(token: string, password: string) : Observable<loginResponse> {
      return this.http.post<loginResponse>(this.securityUrl + 'reset?token=' + token, password);
  }
}
