import { Observable } from "rxjs";
import { EquipmentDTO } from "../DTOs/equipment.dto";

export abstract class EquipmentService {
  abstract getAll(): Observable<EquipmentDTO[]>;
}
