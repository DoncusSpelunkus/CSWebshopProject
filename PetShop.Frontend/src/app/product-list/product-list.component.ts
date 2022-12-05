import { Component, OnInit } from '@angular/core';
import {ProductService} from "../../services/Product.service";
import { Router } from '@angular/router'
import {Observable, Subscription} from "rxjs";
import {Product} from "../../Entities/Product";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit{
  productList: Product[] = [];




  constructor(private http: ProductService, public router: Router) {

  }


  async ngOnInit() {
    this.http.getProductStatus().subscribe(products => this.productList = products)
  }

  UpdateList(): void{
    //this.http.getProductStatus().subscribe(update => {this.productList = update})
  }

  async EditMode(id){


  }

}
