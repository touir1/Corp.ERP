import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { GetListEquipmentComponent } from "./presentation/equipment/get-list-equipment/get-list-equipment.component";
import { InventoryDashboardComponent } from "./presentation/inventory-dashboard/inventory-dashboard.component";

const routes: Routes = [
  {
    path: 'inventory',
    //component: InventoryDashboardComponent,
    children: [
      { path: '', component: InventoryDashboardComponent },
      { path: 'equipments', component: GetListEquipmentComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InventoryRoutingModule { }
