import {Component, OnInit} from '@angular/core';
import {ProductService} from "../../services/Product.service";
import {Router} from "@angular/router";
import { CommService } from "../../services/Commservice";
import {Product} from "../../Entities/Product";

@Component({
  selector: 'app-product-creation',
  templateUrl: './product-creation.component.html',
  styleUrls: ['./product-creation.component.scss']
})
export class ProductCreationComponent implements OnInit{
  product: any;
  sname: number;
  sdesc: string;
  productDeleteId: number;


  constructor(private http: ProductService, public router: Router, private commService: CommService) {
    this.sname = 0;
    this.sdesc = '';
    this.product = Product;
    this.productDeleteId = 0;
  }

  async ngOnInit() {
  }

  createProduct(){
    this.http.addProduct(this.product)
      .subscribe(() => {
        this.router.navigateByUrl('/admin');
      });
    this.updateList();
  }


  deleteProduct(id){
    this.http.DeleteProductByID(id)
      .subscribe(() => {
        this.router.navigateByUrl('/admin');
      });
    this.updateList();
  }

  updateList(){
    this.commService.sendUpdate(true)
  }
}
