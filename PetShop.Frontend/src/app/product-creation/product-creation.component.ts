import { Component, OnInit } from '@angular/core';
import {appValuePair} from "../valuePair";
import {HttpService} from "../../services/http.service";

@Component({
  selector: 'app-product-creation',
  templateUrl: './product-creation.component.html',
  styleUrls: ['./product-creation.component.scss']
})
export class ProductCreationComponent implements OnInit{
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

}
