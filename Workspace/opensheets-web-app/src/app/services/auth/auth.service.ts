import { Injectable } from '@angular/core';
import { StorageService } from '../storage/storage.service';
import { token, tokenType } from 'src/app/models/token';
import { SecurityService } from '../security/security.service';
import { LoginModel } from 'src/app/models/login-model';
import { BehaviorSubject, Observable } from 'rxjs';
import { StorageEventType } from '../storage/StorageUpdateEvent';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
	private loggedInSubject = new BehaviorSubject<boolean>(this.isAuthenticated());

  constructor(private storageService: StorageService, private securityService: SecurityService) {
	  this.storageService.watch().subscribe(evt => {
		if(evt.type == StorageEventType.clear 
			|| ((evt.type == StorageEventType.remove || evt.type == StorageEventType.set) 
				&& (evt.key == 'bearer-token' || evt.key == 'refresh-token')))
		{
			this.pulse();
		}
	  });
   }

  public login(username: string, password: string) : Promise<void> {
	let promise = new Promise<void>((resolve, reject) => {
		let model = new LoginModel();

		model.username = username;
		model.password = password;

		this.securityService.login(model).subscribe(data => {
			data.tokens.forEach(e => {
				switch(e.type){
					case tokenType.bearer:
						this.storageService.set('bearer-token', `bearer ${e.data} ${e.initVal}`);
						break;
					case tokenType.refresh:
						this.storageService.set('refresh-token', `refresh ${e.data} ${e.initVal}`);
						break;
				}
			}, 
			err => {
				reject(err.error);
			});

			resolve();

			this.pulse();
		});
	});	

	return promise;
  }

  public logout() : Promise<void> {
	let promise = new Promise<void>((resolve, reject) => {
		this.storageService.remove('refresh-token');
		this.storageService.remove('bearer-token');

		resolve();

		this.pulse();
	});

	return promise;
  }

  public isAuthenticated() : boolean {
	const bearTokenStr = this.storageService.get('bearer-token');

	let bearToken = new token();

	Object.assign(bearToken, JSON.parse(bearTokenStr));

	if(bearToken && bearToken.expiration.getDate > Date.now)
	{
		return true;
	}

	const refrTokenStr = this.storageService.get('refresh-token');

	let refrToken = new token();

	Object.assign(refrToken, JSON.parse(refrTokenStr));

	if(refrToken && refrToken.expiration.getDate > Date.now)
	{
		return true;
	}

	return false;
  }

  public pulse() : void {
	  this.loggedInSubject.next(this.isAuthenticated());
  }

  public watch() : Observable<boolean>{
	  return this.loggedInSubject.asObservable();
  }
}
