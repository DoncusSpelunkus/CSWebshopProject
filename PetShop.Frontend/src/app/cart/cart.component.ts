import { Component, OnInit } from '@angular/core';
import {CartService} from "../../services/cart.service";
import {Product} from "../../Entities/Product";
import {ProductService} from "../../services/Product.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  products: any[] = [];
  productList: any;
  subTotal!: any;

  constructor(private cartService: CartService, private productService: ProductService, private router: Router) { }

  async ngOnInit() {
    await this.productService.getProducts()
    this.cartService.loadCart();
    this.products = this.cartService.getProduct();
  }

  addToCart(product: any) {
    if (!this.cartService.productInCart(product)) {
      product.quantity = 1;
      this.cartService.addToCart(product);
      this.products = [...this.cartService.getProduct()];
      this.subTotal = product.price;
    }
  }

  removeFromCart(product: any) {
    this.cartService.removeProduct(product);
    this.products = this.cartService.getProduct();
  }

  get total() {
    return this.products?.reduce(
      (sum, product) => ({
        quantity: 1,
        price: sum.price + product.quantity * product.price,
      }),
      { quantity: 1, price: 0 }
    ).price;
  }

  checkout() {
    localStorage.setItem('cart_total', JSON.stringify(this.total));
    this.router.navigate(['/payment']);
  }

}
