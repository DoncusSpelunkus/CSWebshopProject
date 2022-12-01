import {Component, OnInit} from '@angular/core';
import {HttpService} from "../../services/http.service";
import {dto} from "../../Entities/dto";
import {CommService} from "../../services/CommService";
import {Router} from "@angular/router";
import {Subscription} from "rxjs";

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
  currentProduct: any;
  productDeleteId: number;
  private productEditSub: Subscription;


  constructor(private http: HttpService, private Service: CommService, public router: Router) {
    this.sname = 0;
    this.sdesc = '';
    this.dtoi = dto;
    this.productDeleteId = 0;
    this.productEditSub = this.Service.getProductEdit().subscribe //Subscribes to get check for
      (message => {
          this.currentProduct = this.product.find(message)
        console.log(this.currentProduct)
      });
  }

  async ngOnInit() {
    const product = await this.http.GetProduct();
    this.product = product;
  }

  async CreateProduct(){
    const result = await this.http.CreateProduct(this.dtoi);
    this.product.push(result);
    this.dtoi.specList = [];
    this.UpdateList();
  }


  async DeleteProduct(id){
    const result = await this.http.DeleteProductByID(id)
    this.product.push(result);
  }

  async UpdateList(): Promise<void> {
    this.Service.requestProductListUpdate(true);
  }
}
