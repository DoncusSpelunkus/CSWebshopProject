import { Component, OnInit } from '@angular/core';
import {CartService} from "../../../services/cart.service";
import {CartState} from "../../../states/CartState";
import {Order} from "../../../Entities/Order";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  order: any = Order;
  orderList: any;

  constructor(private Aroute: ActivatedRoute, private cartService: CartService, private cartState: CartState) { }

  async ngOnInit() {
    const id = Number(this.Aroute.snapshot.paramMap.get(`id`))
    await this.getOrderById(id);
  }

  async getOrderById(id) {
    this.orderList = await this.cartState.getOrderById(id);
  }

}
