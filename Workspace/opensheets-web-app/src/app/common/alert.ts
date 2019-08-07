import { AlertLevel } from '../enums/alertlevel';
import { Guid } from 'guid-typescript';

export class Alert {
	id: Guid;
	message: string;
	level: AlertLevel;
	canClose: boolean;
}