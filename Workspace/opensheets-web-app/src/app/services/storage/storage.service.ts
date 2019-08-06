import { Injectable, Inject } from '@angular/core';
import {LOCAL_STORAGE, SESSION_STORAGE, WebStorageService} from 'angular-webstorage-service';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

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
	  switch (kind){
		case StorageKind.Local:
			localStorage.setItem(key, value);
			return;
		case StorageKind.Session:
			sessionStorage.setItem(key, value);
			return;
	}
  }

  remove(key: string, kind: StorageKind = StorageKind.Local) : void {
	  switch (kind){
		case StorageKind.Local:
			localStorage.removeItem(key);
			return;
		case StorageKind.Session:
			sessionStorage.removeItem(key);
			return;
	}
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
  }
}

export enum StorageKind {
  Local,
  Session
}