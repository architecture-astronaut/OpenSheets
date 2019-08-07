import { Injectable } from '@angular/core';
import { AlertLevel } from 'src/app/enums/alertlevel';
import { Alert } from 'src/app/common/alert';
import { Guid } from 'guid-typescript';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AlertService {
	alerts: Alert[] = [];
	subject: Subject<Alert[]> = new Subject();

  	constructor() { }

	push(message: string, level: AlertLevel, canClose: boolean, closeTime: int = -1): Promise<Guid> {
		let promise = new Promise<Guid>((resolve, reject) => {
			let alert = new Alert();

			alert.id = Guid.create();
			alert.level = level;
			alert.message = message;
			alert.canClose = canClose;

			this.alerts.push(alert);

			if(closeTime > -1){
				setTimeout(() => { this.clear(alert.id); }, closeTime);
			}

			this.update();

			resolve(alert.id);
		});

		return promise;
	}

	clear(messageId: Guid) : Promise<void> {
		let promise = new Promise<void>((resolve, reject) => {
			this.alerts = this.alerts.filter(e => e.id != messageId);

			this.update();

			resolve();
		})

		return promise;
	}

	update() : void {
		this.subject.next(Object.assign({}, this.alerts));
	}

	get() : Observable<Alert[]> {
		return this.subject.asObservable();
	}
}