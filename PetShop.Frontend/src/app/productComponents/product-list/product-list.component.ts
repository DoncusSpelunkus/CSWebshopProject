import {Component, Input, OnInit} from '@angular/core';
import { Router } from '@angular/router'
import {PseudoLogicSearch} from "../../../states/PseudoLogicSearch";
import { Product} from "../../../Entities/Product";
import {PseudoLogicCart} from "../../../states/PseudoLogicCart";
import {Order} from "../../../Entities/Order";


@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit{
  product: any = Product;
  productList: any;
  order: any = Order;
  @Input() name = '';

  constructor(private searchState: PseudoLogicSearch, private cartState: PseudoLogicCart, public router: Router) {

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

  async postOrder(id: number, price: number) {
    await this.cartState.postOrder(id, price)
  }
}
