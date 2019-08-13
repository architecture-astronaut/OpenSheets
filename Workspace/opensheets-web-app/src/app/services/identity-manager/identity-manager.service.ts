import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { Identity } from 'src/app/models/partial-identity';

@Injectable({
  providedIn: 'root'
})
export class IdentityManagerService {
	private _identitySubject = new BehaviorSubject<Identity>(null);
	
	constructor() { }

	getCurrentIdentity() : Observable<Identity> {
		return this._identitySubject.asObservable();
	}

	setCurrentIdentity(identity: Identity) : void {
		this._identitySubject.next(identity);
	}
}