import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { PartialIdentity } from '../models/partial-identity';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  private apiPath = 'api/identity/';
  constructor(private http: HttpClient) { }

  getMyIdentities() : Observable<PartialIdentity[]> {
	return this.http.get<PartialIdentity>(this.apiPath + "my", )
  }
}
