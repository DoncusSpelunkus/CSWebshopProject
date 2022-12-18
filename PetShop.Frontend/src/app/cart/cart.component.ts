import { Component, OnInit } from '@angular/core';
import {CartService} from "../../services/cart.service";
import {ProductService} from "../../services/Product.service";
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
    console.log(this.orderList)
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      if (decodToken.id)
      this.orderList = await this.cartState.getOrders(decodToken.id);
    }
    console.log(this.orderList)
  }

  async putOrder(id: number, amount: number, price: number){
    await this.cartState.putOrder(id, amount, price)
  }

  async deleteOrderByID(id){
    await this.cartState.deleteOrderByID(id);
  }

  async placeOrder(id){
    await this.cartState.placeOrder(id);
  }


  async SendOrderMail(userEmail: String) { //

  }


}
