import {Component, OnInit} from '@angular/core';
import {HttpService} from "../../services/http.service";
import {dto} from "../../Entities/dto";
import { appRoutingModule } from "../app.router";

@Component({
  selector: 'app-product-creation',
  templateUrl: './product-creation.component.html',
  styleUrls: ['./product-creation.component.scss']
})
export class ProductCreationComponent implements OnInit{
  product: any;
  sname: number;
  sdesc: string;
  dtoi: any;
  productDeleteId: number;


  constructor(private http: HttpService) {
    this.sname = 0;
    this.sdesc = '';
    this.dtoi = dto;
    this.productDeleteId = 0;
  }

  async ngOnInit() {
    const product = await this.http.GetProduct();
    this.product = product;
  }

  async CreateProduct(){
    const result = await this.http.CreateProduct(this.dtoi);
    this.product.push(result);
    this.dtoi.specList = [];
  }

  async DeleteProduct(id){
    const result = await this.http.DeleteProductByID(id)
    this.product.push(result);
  }
}
