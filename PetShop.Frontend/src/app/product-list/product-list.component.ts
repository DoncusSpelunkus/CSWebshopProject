import { Component, OnInit } from '@angular/core';
import {appValuePair} from "../../Entities/valuePair";
import {HttpService} from "../../services/http.service";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit{
  product: any;

  constructor(private http: HttpService) {
  }

  async ngOnInit() {
    const product = await this.http.GetProduct();
    this.product = product;
  }

}
