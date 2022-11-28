import {Component, OnInit} from '@angular/core';
import {HttpService} from "../services/http.service";
import {appValuePair} from "./valuePair";
import {MatSelectChange} from "@angular/material/select";

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
  specNames: any;
  specList: Array<appValuePair> = [];


  constructor(private http: HttpService) {
    let sname = 0;
    this.sname = sname;
    let sdesc = ``;
    this.sdesc = sdesc;
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

  async DeleteProductByID(id: any) {
    const product = await this.http.DeleteProductByID(id);
    this.product = this.product.filter(p => p.id != product.id)
  }

  async AttachSpecs(spec: number, name: string){
    let valuePair = new appValuePair(spec,name);
    this.specList.push(valuePair)
  }

  async ChangeDrop($event: MatSelectChange){

  }
}
