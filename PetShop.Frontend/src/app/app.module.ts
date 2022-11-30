import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

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
import { HeaderComponent } from './header/header.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import { ProductCreationComponent } from './product-creation/product-creation.component';
import { ProductListComponent } from './product-list/product-list.component';
import { DeleteButtonComponent } from './delete-button/delete-button.component';
import { HomeComponent } from './home/home.component';
import { appRoutingModule } from './app.router';
import { AdminComponent } from './admin/admin.component'



@NgModule({
  declarations: [
    AppComponent,
    ProductCreationComponent,
    HeaderComponent,
    ProductCreationComponent,
    ProductListComponent,
    DeleteButtonComponent,
    HomeComponent,
    AdminComponent,
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
    MatSidenavModule
  ],
  providers: [MatSnackBar, Overlay],
  bootstrap: [AppComponent]
})
export class AppModule { }
