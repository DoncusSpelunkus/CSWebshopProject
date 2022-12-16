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

  constructor(private Aroute: ActivatedRoute, private adminState: AdminState, public router: Router, private cartService: CartService) {

  }

  async ngOnInit() {
    const id = Number(this.Aroute.snapshot.paramMap.get(`id`))
    this.product = await this.adminState.getProductById(id);
    this.MainCat = await this.adminState.getCategoryById(this.product.mainCategory, "MainCat")
    this.SubCat = await this.adminState.getCategoryById(this.product.subCategory, "SubCat")
    this.Brand = await this.adminState.getCategoryById(this.product.brand, "Brand")
  }


}
