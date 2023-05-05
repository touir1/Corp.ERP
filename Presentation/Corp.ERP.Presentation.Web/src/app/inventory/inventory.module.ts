import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EquipmentService } from './application/equipment/base/equipment.service';
import { EquipmentServiceImpl } from './application/equipment/equipment.service.impl';
import { GetListEquipmentComponent } from './presentation/equipment/get-list-equipment/get-list-equipment.component';
import { EquipmentApi } from './infrastructure/equipment/equipment.api';
import { EquipmentRepository } from './domain/equipment/equipment.repository';
import { EquipmentRepositoryImpl } from './infrastructure/equipment/equipment.repository.impl';
import { EquipmentMapper } from './application/equipment/equipment.mapper';
import { GetEquipmentsUseCase } from './application/equipment/use-cases/get-equipments.use-case';
import { InventoryDashboardComponent } from './presentation/inventory-dashboard/inventory-dashboard.component';
import { InventoryRoutingModule } from './inventory-routing.module';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    InventoryRoutingModule
  ],
  declarations: [
    // declare any components, directives, or pipes here
    GetListEquipmentComponent,
    InventoryDashboardComponent
  ],
  providers: [
    // provide any services or dependencies here
    EquipmentMapper,
    EquipmentApi,
    { provide: EquipmentRepository, useClass: EquipmentRepositoryImpl },
    GetEquipmentsUseCase,
    { provide: EquipmentService, useClass: EquipmentServiceImpl },
    
  ]
})
export class InventoryModule { }
