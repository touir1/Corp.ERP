import { User } from "../user/user.entity";
import { StorageUnit } from "../storage-unit/storage-unit.entity";

export interface Equipment {
  Id: string;
  Name: string;
  Description: string;
  Code: string;
  IsInUse: boolean;
  StartDateUsage: Date;
  UsedBy: User;
  StorageUnit: StorageUnit;
  
}
