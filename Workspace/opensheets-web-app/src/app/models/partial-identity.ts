import { Guid } from "guid-typescript";
import { IdentityKind } from './identity-kind';

export class PartialIdentity
{
    public id : Guid;
    public name : string;
    public kind : IdentityKind;
}