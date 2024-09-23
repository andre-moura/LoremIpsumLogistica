import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerListComponent } from './features/customers/components/customer-list/customer-list.component';
import { CustomerAddressesComponent } from './features/customers/components/customer-addresses/customer-addresses.component';
// Import other components as needed

const routes: Routes = [
  { path: '', component: CustomerListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
