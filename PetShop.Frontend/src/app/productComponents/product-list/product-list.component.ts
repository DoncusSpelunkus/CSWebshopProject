import {Component, Input, OnInit} from '@angular/core';
import { Router } from '@angular/router'
import {SearchState} from "../../../states/SearchState";
import {Category} from "../../../Entities/Category";
import {AdminState} from "../../../states/AdminState";
import { CartService} from "../../../services/cart.service";
import { Product} from "../../../Entities/Product";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit{
  product: any = Product;
  productList: any;
  @Input() name = '';

  constructor(private searchState: SearchState, private adminState: AdminState, private cartService: CartService, public router: Router) {

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

  addToCart(product: any) {
    if (!this.cartService.productInCart(product)){
      this.cartService.addToCart(product);
      this.product = [this.cartService.getProduct()];
    }
    window.alert('Your product has been added to the cart!');
  }
}
