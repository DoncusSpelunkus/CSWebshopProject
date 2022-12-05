import {Component, OnInit} from '@angular/core';
import {ProductService} from "../../services/Product.service";
import {Router} from "@angular/router";
import {Subscription} from "rxjs";
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


  constructor(private http: ProductService, public router: Router) {
    this.sname = 0;
    this.sdesc = '';
    this.product = Product;
    this.productDeleteId = 0;
  }

  async ngOnInit() {
  }

  CreateProduct(){
    this.http.addProduct(this.product)
      .subscribe(() => {
        this.router.navigateByUrl('/products');
      });
  }


  async DeleteProduct(id){
    const result = await this.http.DeleteProductByID(id)
    this.product.push(result);
  }
}
