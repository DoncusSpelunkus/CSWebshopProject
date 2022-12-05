import {Component, OnInit} from '@angular/core';
import {ProductService} from "../services/Product.service";
import {appRoutingModule} from "./app.router";

// @ts-ignore
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{


  constructor(private http: ProductService) {
    appRoutingModule
  }

  async ngOnInit() {
  }

}
