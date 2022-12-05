import {Component, OnInit} from '@angular/core';

import {ProductService} from "../services/Product.service";
import {appRoutingModule} from "./app.router";

// @ts-ignore
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  name: string = '';
  price: number = 0;
  description: string = '';
  imageUrl: string = '';
  rating: number = 0;
  specs: { [n: number]: string; } | undefined;
  mainCategory: number = 0;
  subCategory: number = 0;
  brand: number = 0;
  product: any;
  sname: number;
  sdesc: string;
  specList: Array<appValuePair> = [];



  constructor(private http: ProductService) {
    appRoutingModule
  }

  async ngOnInit() {
    const product = await this.http.GetProduct();
    this.product = product;

  }

  async CreateProduct(){
    let dto = {
      name: this.name,
      price: this.price,
      description: this.description,
      imageUrl: this.imageUrl,
      rating: this.rating,
      specs: this.specs,
      mainCategory: this.mainCategory,
      subCategory: this.subCategory,
      brand: this.brand,
      specList: this.specList,
    }
    const result = await this.http.CreateProduct(dto);
    this.product.push(result);
    this.specList = [];
  }

  async DeleteProductByID(productID: any) {
    const product = await this.http.DeleteProductByID(productID);
    this.product = this.product.filter(p => p.productID != product.productID)
  }

  async AttachSpecs(spec: number, name: string){
    let valuePair = new appValuePair(spec,name);
    this.specList.push(valuePair)
  }
}
