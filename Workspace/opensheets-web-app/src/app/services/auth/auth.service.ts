import { Injectable } from '@angular/core';
import { StorageService } from '../storage/storage.service';
import { token, tokenType } from 'src/app/models/token';
import { SecurityService } from '../security/security.service';
import { LoginModel } from 'src/app/models/login-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private storageService: StorageService, private securityService: SecurityService) { }

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
		});
	});	

	return promise;
  }

  public logout() : Promise<void> {
	let promise = new Promise<void>((resolve, reject) => {
		this.storageService.remove('refresh-token');
		this.storageService.remove('bearer-token');

		resolve();
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
}
