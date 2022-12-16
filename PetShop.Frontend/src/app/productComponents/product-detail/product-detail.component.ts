import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router'
import {AdminState} from "../../../states/AdminState";
import { CartService} from "../../../services/cart.service";
import { Product} from "../../../Entities/Product";
import {Category} from "../../../Entities/Category";

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  product: any = Product;
  MainCat: any = Category;
  SubCat: any = Category;
  Brand: any = Category;

  constructor(private route: ActivatedRoute, private adminState: AdminState, public router: Router, private cartService: CartService) {

  }

  async ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get(`id`))
    this.product = await this.adminState.getProductById(id);
    this.MainCat = await this.adminState.getCategoryById(this.product.mainCategory, "MainCat")
    this.SubCat = await this.adminState.getCategoryById(this.product.subCategory, "SubCat")
    this.Brand = await this.adminState.getCategoryById(this.product.brand, "Brand")
  }

  addToCart(product: any) {
    if (!this.cartService.productInCart(product)){
      this.cartService.addToCart(product);
      this.product = [this.cartService.getProduct()];
    }
    window.alert('Your product has been added to the cart!');
  }
}
