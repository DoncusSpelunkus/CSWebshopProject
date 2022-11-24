import {Component, OnInit} from '@angular/core';
import {HttpService} from "../services/http.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  boxLength: number = 0;
  boxWidth: number = 0;
  boxHeight: number = 0;
  product: any;

  constructor(private http: HttpService) {

  }

  async ngOnInit() {
    const boxes = await this.http.GetBoxes();
    this.product = product;
  }

  async createBox(){
    let dto = {
      length: this.boxLength,
      width: this.boxWidth,
      height: this.boxHeight,
    }
    const result = await this.http.createBox(dto);
    this.product.push(result);
  }

  async deleteBox(productID: any) {
    const product = await this.http.deleteBox(productID);
    this.product = this.product.filter(p => p.productID != product.productID)
  }
}
