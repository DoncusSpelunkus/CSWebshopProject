import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './Pages/home/home.component';
import { AdminComponent } from "./Pages/admin/admin.component";
import { ProductEditComponent } from "./product-related/product-edit/product-edit.component";
import {
  SpecificationUpdateComponent
} from "./specification-related/specification-update/specification-update.component";
import {LoginComponent} from "./Pages/login/login.component";

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'admin', component: AdminComponent},
  { path: 'product-edit/:id', component: ProductEditComponent},
  { path: 'specification-update/:id', component: SpecificationUpdateComponent},
  { path: 'login', component: LoginComponent},

  // otherwise redirect to home
  { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);
