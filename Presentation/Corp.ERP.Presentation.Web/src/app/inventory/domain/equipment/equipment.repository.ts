import { Observable } from "rxjs";
import { Equipment } from "./equipment.entity";


export abstract class EquipmentRepository {
  abstract getAll(): Observable<Equipment[]>;
  abstract getById(id: string): Observable<Equipment>;
  abstract add(equipment: Equipment): Observable<Equipment>;
  abstract update(equipment: Equipment): Observable<Equipment>;
  abstract delete(id: string): Observable<void>;
}
