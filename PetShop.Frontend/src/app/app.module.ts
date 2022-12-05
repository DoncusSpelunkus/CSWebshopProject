import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from "@angular/common/http";
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {FormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatCardModule} from "@angular/material/card";
import {MatListModule} from "@angular/material/list";
import {MatTooltipModule} from "@angular/material/tooltip";
import {Overlay} from "@angular/cdk/overlay";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatTableModule} from "@angular/material/table";
import {MatSelectModule} from "@angular/material/select";
import { HeaderComponent } from './Navigation-tools/header/header.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import { ProductCreationComponent } from './product-related/product-creation/product-creation.component';
import { ProductListComponent } from './product-related/product-list/product-list.component';
import { HomeComponent } from './Pages/home/home.component';
import { appRoutingModule } from './app.router';
import { AdminComponent } from './Pages/admin/admin.component';
import { ProductEditComponent } from './product-related/product-edit/product-edit.component'
import { AgGridModule } from "ag-grid-angular";
import { CategoryCreationComponent } from './category-related/category-creation/category-creation.component';
import {MatTabsModule} from "@angular/material/tabs";
import { CategoryEditComponent } from './category-related/category-edit/category-edit.component';
import { CategoryListComponent } from './category-related/category-list/category-list.component';



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
    MatTabsModule
  ],
  providers: [MatSnackBar, Overlay],
  bootstrap: [AppComponent]
})
export class AppModule { }
