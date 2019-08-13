import { StorageKind } from './storage.service';

export class StorageEvent {

	constructor(public key: string, public oldValue: string, public newValue: string, public type: StorageEventType, public storageKind: StorageKind){}
} 

export enum StorageEventType {
	set,
	remove,
	clear,
	initialize
}