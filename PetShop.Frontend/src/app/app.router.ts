import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './landingPages/home/home.component';
import { AdminComponent } from "./landingPages/admin/admin.component";
import { UserComponent } from "./landingPages/user/user.component";
import { ProductEditComponent } from "./productComponents/product-edit/product-edit.component";
import {
  SpecificationUpdateComponent
} from "./specificationComponents/specification-update/specification-update.component";
import {LoginComponent} from "./landingPages/login/login.component";
import {UserRegistrationComponent} from "./landingPages/user-registration/user-registration.component";
import {AuthGuardService} from "../services/AuthGuardService";
import {LoginGuardService} from "../services/LoginGuardService";
import {CategoryEditComponent} from "./categoryComponents/category-edit/category-edit.component";
import {CartComponent} from "./cart/cart.component";
import {ProductDetailComponent} from "./productComponents/product-detail/product-detail.component";
import {UserGuard} from "../services/UserGuard";

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuardService]},
  { path: 'user', component: UserComponent, canActivate: [UserGuard]},
  { path: 'product-edit/:id', component: ProductEditComponent, canActivate: [AuthGuardService]},
  { path: 'specification-update/:id', component: SpecificationUpdateComponent, canActivate: [AuthGuardService]},
  { path: 'category/:path/:id', component: CategoryEditComponent, canActivate: [AuthGuardService]},
  { path: 'login', component: LoginComponent, canActivate: [LoginGuardService]},
  { path: 'register', component: UserRegistrationComponent, canActivate: [LoginGuardService] },
  { path: 'product/:id', component: ProductDetailComponent},
  { path: 'cart', component: CartComponent},
  // otherwise redirect to home
  { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);
