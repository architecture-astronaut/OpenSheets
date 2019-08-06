import { Timestamp } from 'rxjs/internal/operators/timestamp';
import { Guid } from 'guid-typescript';

export class token {
	principalId: Guid;
	type: tokenType;
	issued: Date;
	expiration: Date;
	data: string;
	initVal: string;
}

export class loginResponse {
	tokens: token[];
}

export enum tokenType {
	bearer,
	refresh,
	reset
}