import {Component, OnInit} from '@angular/core';
import {HttpService} from "../services/http.service";

// @ts-ignore
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  name: string = '';
  price: number = 0;
  description: string = '';
  imageUrl: string = '';
  rating: number = 0;
  specs: { [n: number]: string; } | undefined;
  mainCategory: number = 0;
  subCategory: number = 0;
  brand: number = 0;
  product: any;

  constructor(private http: HttpService) {

  }

  async ngOnInit() {
    const product = await this.http.GetProduct();
    this.product = product;
  }

  async CreateProduct(){
    let dto = {
      name: this.name,
      price: this.price,
      description: this.description,
      imageUrl: this.imageUrl,
      rating: this.rating,
      specs: this.specs,
      mainCategory: this.mainCategory,
      subCategory: this.subCategory,
      brand: this.brand,

    }
    const result = await this.http.CreateProduct(dto);
    this.product.push(result);
  }

  async DeleteProductByID(productID: any) {
    const product = await this.http.DeleteProductByID(productID);
    this.product = this.product.filter(p => p.productID != product.productID)
  }
}
