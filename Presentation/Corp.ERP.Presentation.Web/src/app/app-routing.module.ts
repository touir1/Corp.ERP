import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { InventoryDashboardComponent } from './inventory/presentation/inventory-dashboard/inventory-dashboard.component';
import { HomeComponent } from './home/presentation/home.component';
import { InventoryRoutingModule } from './inventory/inventory-routing.module';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  {
    path: 'inventory',
    //component: InventoryDashboardComponent,
    loadChildren: () => import('./inventory/inventory.module').then(m => m.InventoryModule),
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
