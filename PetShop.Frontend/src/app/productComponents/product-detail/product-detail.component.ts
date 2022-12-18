import {Component, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router'
import {AdminState} from "../../../states/AdminState";
import { CartState } from "../../../states/CartState";
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
  rating: number = 0;
  @ViewChild('child') child;

  constructor(private route: ActivatedRoute, private adminState: AdminState, public router: Router, private cartState: CartState) {

  }

  async ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get(`id`))
    this.product = await this.adminState.getProductById(id);
    this.MainCat = await this.adminState.getCategoryById(this.product.mainCategory, "MainCat")
    this.SubCat = await this.adminState.getCategoryById(this.product.subCategory, "SubCat")
    this.Brand = await this.adminState.getCategoryById(this.product.brand, "Brand")
    this.product.ratings = 5;
    this.child.setProductId(this.product.id)
  }

  async postOrder(id: number, price: number) {
    await this.cartState.postOrder(id, price)
  }

}
