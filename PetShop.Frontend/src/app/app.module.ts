import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from "@angular/common/http";
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatCardModule} from "@angular/material/card";
import {MatListModule} from "@angular/material/list";
import {MatTooltipModule} from "@angular/material/tooltip";
import {Overlay} from "@angular/cdk/overlay";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatTableModule} from "@angular/material/table";
import {MatSelectModule} from "@angular/material/select";
import { HeaderComponent } from './navigationComponents/header/header.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import { ProductCreationComponent } from './productComponents/product-creation/product-creation.component';
import { ProductListComponent } from './productComponents/product-list/product-list.component';
import { HomeComponent } from './landingPages/home/home.component';
import { appRoutingModule } from './app.router';
import { AdminComponent } from './landingPages/admin/admin.component';
import { ProductEditComponent } from './productComponents/product-edit/product-edit.component'
import { AgGridModule } from "ag-grid-angular";
import { CategoryCreationComponent } from './categoryComponents/category-creation/category-creation.component';
import {MatTabsModule} from "@angular/material/tabs";
import { CategoryEditComponent } from './categoryComponents/category-edit/category-edit.component';
import { CategoryListComponent } from './categoryComponents/category-list/category-list.component';
import { SpecificationCreationComponent } from './specificationComponents/specification-creation/specification-creation.component';
import { SpecificationListComponent } from './specificationComponents/specification-list/specification-list.component';
import { SpecificationUpdateComponent } from './specificationComponents/specification-update/specification-update.component';
import { CurrentSpecListComponent } from './specificationComponents/current-spec-list/current-spec-list.component';
import { LoginComponent } from './landingPages/login/login.component';
import { UserComponent } from './landingPages/user/user.component';
import { UserEditComponent } from './userComponents/user-edit/user-edit.component';
import { UserRegistrationComponent } from './landingPages/user-registration/user-registration.component';
import {MatGridListModule} from "@angular/material/grid-list";
import {Ng2SearchPipeModule} from "ng2-search-filter";



@NgModule({
  declarations: [
    AppComponent,
    ProductCreationComponent,
    HeaderComponent,
    ProductCreationComponent,
    ProductListComponent,
    HomeComponent,
    AdminComponent,
    ProductEditComponent,
    CategoryCreationComponent,
    CategoryEditComponent,
    CategoryListComponent,
    SpecificationCreationComponent,
    SpecificationListComponent,
    SpecificationUpdateComponent,
    CurrentSpecListComponent,
    LoginComponent,
    UserComponent,
    UserEditComponent,
    UserRegistrationComponent,

  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    appRoutingModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatCardModule,
    MatListModule,
    MatTooltipModule,
    MatTableModule,
    MatSelectModule,
    MatSidenavModule,
    HttpClientModule,
    AgGridModule,
    MatTabsModule,
    MatGridListModule,
    Ng2SearchPipeModule
  ],
  providers: [MatSnackBar, Overlay],
  bootstrap: [AppComponent]
})
export class AppModule { }
