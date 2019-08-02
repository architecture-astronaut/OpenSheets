import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

import { SecurityService } from '../security/security.service';
import { HttpStatusCode } from '../enums/http-status-code';
import { StorageService, StorageKind } from '../storage/storage.service';
import { ErrorCode } from '../enums/error-code';

@Injectable()
export class SessionRecoveryInterceptor implements HttpInterceptor {
	private _refreshSubject: Subject<any> = new Subject<any>();

	constructor(private storageService: StorageService, private securityService: SecurityService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(req);
	}
}