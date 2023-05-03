import { forwardRef, Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { Equipment } from "../../domain/equipment/equipment.entity";
import { EquipmentRepository } from "../../domain/equipment/equipment.repository";
import { InventoryModule } from "../../inventory.module";
import { EquipmentApi } from "./equipment.api";
import { GetAllEquipmentsRequest } from "./requests/get-all-equipments.request";

@Injectable({
  providedIn: forwardRef(() => InventoryModule)
})
export class EquipmentRepositoryImpl implements EquipmentRepository {

  constructor(private api: EquipmentApi) {
        
  }

  getAll(): Observable<Equipment[]> {
    return this.api.getAll(new GetAllEquipmentsRequest()).pipe(
      map(response => response.Equipments)
    );
  }

  getById(id: string): Observable<Equipment> {
    throw new Error("Method not implemented.");
  }
  add(equipment: Equipment): Observable<Equipment> {
    throw new Error("Method not implemented.");
  }
  update(equipment: Equipment): Observable<Equipment> {
    throw new Error("Method not implemented.");
  }
  delete(id: string): Observable<void> {
    throw new Error("Method not implemented.");
  }

}
