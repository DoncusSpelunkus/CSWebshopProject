import { Component, OnInit } from '@angular/core';
import {ProductService} from "../../services/Product.service";
import { Router } from '@angular/router'
import { CommService } from "../../services/Commservice";
import {Product} from "../../Entities/Product";
import {Subject, Subscription} from "rxjs";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit{
  productList: Product[] = [];
  private updateCallSub: Subscription;

  constructor(private http: ProductService, public router: Router, private commService: CommService) {
    this.updateCallSub= this.commService.getUpdate().subscribe
    (message => { //message contains the data sent from service
      if(message === true){
        this.ngOnInit();
      }
    });
  }


  async ngOnInit() {
    this.updateList()
  }

  updateList(): void{
    this.http.getProductStatus().subscribe(products => this.productList = products)
  }
}
