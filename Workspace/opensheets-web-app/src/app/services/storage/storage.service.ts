import { Injectable, Inject } from '@angular/core';
import {LOCAL_STORAGE, SESSION_STORAGE, WebStorageService} from 'angular-webstorage-service';
import { BehaviorSubject, Observable } from 'rxjs';
import { StorageEvent, StorageEventType } from './StorageUpdateEvent';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
	private _updatedSubject = new BehaviorSubject<StorageEvent>(new StorageEvent(null, null, null, StorageEventType.initialize, null));

  constructor(@Inject(LOCAL_STORAGE) private localStorage: WebStorageService, @Inject(SESSION_STORAGE) private sessionStorage: WebStorageService) { }

  get(key: string, kind: StorageKind = StorageKind.Local) : string {
	switch (kind){
		case StorageKind.Local:
			return localStorage.getItem(key);
		case StorageKind.Session:
			return sessionStorage.getItem(key);
	}
  }

  set(key: string, value: string, kind: StorageKind = StorageKind.Local) : void {
		let oldVal: string;
	  switch (kind){
		case StorageKind.Local:
			oldVal = localStorage.getItem(key);
			localStorage.setItem(key, value);
			return;
		case StorageKind.Session:
			oldVal =sessionStorage.getItem(key);
			sessionStorage.setItem(key, value);
			return;
	}

		this._updatedSubject.next(new StorageEvent(key, oldVal, value, StorageEventType.remove, kind));
  }

  remove(key: string, kind: StorageKind = StorageKind.Local) : void {
		let oldVal: string;

	  switch (kind){
		case StorageKind.Local:
			oldVal = localStorage.getItem(key);
			localStorage.removeItem(key);
			return;
		case StorageKind.Session:
			oldVal =sessionStorage.getItem(key); 
			sessionStorage.removeItem(key);
			return;
	}

	this._updatedSubject.next(new StorageEvent(key, oldVal, null, StorageEventType.remove, kind));
  }

  clear(kind: StorageKind) : void {
	  switch (kind){
		case StorageKind.Local:
			localStorage.clear();
			return;
		case StorageKind.Session:
			sessionStorage.clear();
			return;
	}

	this._updatedSubject.next(new StorageEvent(null, null, null, StorageEventType.clear, kind));
  }

  watch() : Observable<StorageEvent>{
	  return this._updatedSubject.asObservable();
  }
}

export enum StorageKind {
  Local,
  Session
}