import {Component, OnInit} from '@angular/core';
import {HttpService} from "../../services/http.service";
import {dto} from "../../Entities/dto";

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


  constructor(private http: HttpService) {
    this.sname = 0;
    this.sdesc = ``;
    this.dtoi = dto;
  }

  async ngOnInit() {
    const product = await this.http.GetProduct();
    this.product = product;
  }

  async CreateProduct(){
    console.log(this.dtoi.imageUrl)
    const result = await this.http.CreateProduct(this.dtoi);
    this.product.push(result);
    this.dtoi.specList = [];
  }
}
