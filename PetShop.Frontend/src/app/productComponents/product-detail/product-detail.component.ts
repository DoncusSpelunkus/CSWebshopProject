import {Component, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router'
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";
import { PseudoLogicCart } from "../../../states/PseudoLogicCart";
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

  constructor(private route: ActivatedRoute, private adminState: PsuedoLogicAdmin, public router: Router, private cartState: PseudoLogicCart) {

  }

  async ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get(`id`))
    this.product = await this.adminState.getProductById(id);
    this.MainCat = await this.adminState.getCategoryById(this.product.mainCategory, "MainCat")
    this.SubCat = await this.adminState.getCategoryById(this.product.subCategory, "SubCat")
    this.Brand = await this.adminState.getCategoryById(this.product.brand, "Brand")
    this.child.setProductId(this.product.id)
    console.log(this.product.ratings)
  }

  async postOrder(id: number, price: number) {
    await this.cartState.postOrder(id, price)
  }

}
