import {Component, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router'
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";
import { PseudoLogicCart } from "../../../states/PseudoLogicCart";
import { Product} from "../../../Entities/Product";
import {Category} from "../../../Entities/Category";
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";

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
  specList: CurrentSpecs[] = [];

  @ViewChild('child') child;

  constructor(private route: ActivatedRoute, private psuedoLogicAdmin: PsuedoLogicAdmin, public router: Router, private pseudoLogicCart: PseudoLogicCart) {

  }

  async ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get(`id`))
    this.product = await this.psuedoLogicAdmin.getProductById(id);
    this.MainCat = await this.psuedoLogicAdmin.getCategoryById(this.product.mainCategory, "MainCat")
    this.SubCat = await this.psuedoLogicAdmin.getCategoryById(this.product.subCategory, "SubCat")
    this.Brand = await this.psuedoLogicAdmin.getCategoryById(this.product.brand, "Brand")
    this.child.setProductId(this.product.id)
    this.specList = await this.psuedoLogicAdmin.getCurrentSpeclist(this.product.specsDescriptions)
  }

  async postOrder(id: number, price: number) {
    await this.pseudoLogicCart.postOrder(id, price)
  }

}
