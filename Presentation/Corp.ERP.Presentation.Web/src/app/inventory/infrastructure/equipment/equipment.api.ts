import { HttpClient } from '@angular/common/http';
import { forwardRef, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { InventoryModule } from '../../inventory.module';
import { GetAllEquipmentsResponse } from './responses/get-all-equipments.response';
import { GetAllEquipmentsRequest } from './requests/get-all-equipments.request';


@Injectable({
  providedIn: forwardRef(() => InventoryModule)
})
export class EquipmentApi {
  private readonly BASE_URL = environment.apiUrl + '/inventory/Equipments';

  constructor(private readonly http: HttpClient) { }

  getAll(request: GetAllEquipmentsRequest): Observable<GetAllEquipmentsResponse> {
    return this.http.get<GetAllEquipmentsResponse>(this.BASE_URL, {
      params: {

      }
    });
  }
}
