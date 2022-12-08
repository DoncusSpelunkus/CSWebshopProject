import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import {AdminState} from "../../../states/AdminState";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit{
  productList: any;

  constructor(private adminState: AdminState, public router: Router) {

  }

  async ngOnInit() {
    await this.getProducts();
  }

  async getProducts(){
    this.productList = await this.adminState.getProducts();
  }
}
