import { Component, OnInit } from '@angular/core';
import {appValuePair} from "../../Entities/valuePair";
import {HttpService} from "../../services/http.service";
import { Router } from '@angular/router'

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit{
  product: any;

  constructor(private http: HttpService, public router: Router) {
  }

  async ngOnInit() {
    const product = await this.http.GetProduct();
    this.product = product;
  }

}
