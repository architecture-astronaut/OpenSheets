import { Guid } from "guid-typescript";

export class PartialIdentity
{
    public id : Guid;
    public name : string;
    public kind : IdentityKind;
}

export enum IdentityKind {
    player,
    author
}

export class Identity{
	
}