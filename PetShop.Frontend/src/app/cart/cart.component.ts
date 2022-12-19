import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Order} from "../../Entities/Order";
import { CartState} from "../../states/CartState";
import jwtDecode from "jwt-decode";
import {User} from "../../Entities/User";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  order: any = Order;
  orderList: Order[] = [];

  constructor(private Aroute: ActivatedRoute, private cartState: CartState, private router: Router) { }

  async ngOnInit() {
    await this.getOrders();
  }

  async getOrders(){
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      if (decodToken.id)
      this.orderList = await this.cartState.getOrders(decodToken.id);
    }
  }

  async putOrder(id, amount, price){
    await this.cartState.putOrder(id, amount, price)
    console.log(amount)
  }

  async deleteOrderByID(id, productId){
    await this.cartState.deleteOrderByID(id, productId);
    await this.getOrders();
  }

  async placeOrder(id){
    await this.cartState.placeOrder(id);
    await this.getOrders();
  }


  async sendOrderMail(userEmail) { //
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      if (decodToken.email)
        this.orderList = await this.cartState.sendOrderMail(decodToken.email);
    }
    await this.cartState.sendOrderMail(userEmail)
    console.log(userEmail)
  }

}
