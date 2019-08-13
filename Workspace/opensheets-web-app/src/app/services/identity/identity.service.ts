import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { PartialIdentity } from '../../models/partial-identity';
import { Identity } from 'src/app/models/identity';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  private apiPath = 'api/identity/';
  constructor(private http: HttpClient) { }

  getMyIdentities() : Observable<PartialIdentity[]> {
	return this.http.get<PartialIdentity[]>(this.apiPath + 'my', )
  }
  
  private _identitySubject = new BehaviorSubject<Identity>(null);

	getCurrentIdentity() : Observable<Identity> {
		return this._identitySubject.asObservable();
	}

	setCurrentIdentity(identity: Identity) : void {
		this._identitySubject.next(identity);
	}
}
