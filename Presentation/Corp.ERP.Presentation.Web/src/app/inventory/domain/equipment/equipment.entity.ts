import { User } from "../user/user.entity";
import { StorageUnit } from "../storage-unit/storage-unit.entity";

export interface Equipment {
  id: string;
  name: string;
  description: string;
  code: string;
  isInUse: boolean;
  startDateUsage: Date;
  usedBy: User;
  storageUnit: StorageUnit;
  
}
