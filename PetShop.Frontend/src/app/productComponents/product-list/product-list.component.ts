import {Component, Input, OnInit} from '@angular/core';
import { Router } from '@angular/router'
import {SearchState} from "../../../states/SearchState";
import {Category} from "../../../Entities/Category";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit{
  productList: any;

  @Input() name = '';

  constructor(private searchState: SearchState, public router: Router) {

  }

  async ngOnInit() {
    await this.getProducts();
  }

  async getProducts(){
    this.productList = await this.searchState.getProducts();
  }

  async catSort(){
    this.productList = await this.searchState.catSort();
  }

  async priceSort(){
    this.productList = await this.searchState.priceSort();
  }
}
