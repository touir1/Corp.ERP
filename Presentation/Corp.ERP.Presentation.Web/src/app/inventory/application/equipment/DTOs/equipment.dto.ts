import { StorageUnitDTO } from "./storage-unit.dto";
import { UserDTO } from "./user.dto";

export interface EquipmentDTO {
  id: string;
  name: string;
  description: string;
  code: string;
  isInUse: boolean;
  startDateUsage: Date;
  usedBy: UserDTO;
  storageUnit: StorageUnitDTO;
}
