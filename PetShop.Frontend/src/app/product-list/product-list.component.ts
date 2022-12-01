import { Component, OnInit } from '@angular/core';
import {appValuePair} from "../../Entities/valuePair";
import {HttpService} from "../../services/http.service";
import { Router } from '@angular/router'
import {CommService} from "../../services/CommService";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit{
  product: any;
  messageReceived: any;
  private productListSub: Subscription;

  constructor(private http: HttpService, public router: Router, private Service: CommService) {
    this.productListSub = this.Service.getProductListUpdateRequest().subscribe //Subscribes to get check for
    (message => {
      if (message === true){
        this.UpdateList()
      }
    });
  }

  async ngOnInit() {
    const product = await this.http.GetProduct();
    this.product = product;
  }

  async UpdateList(){
    const product = await this.http.GetProduct();
    this.product = product;
    this.messageReceived = false;
  }

  async EditMode(id){

  }

}
