import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Order} from "../../Entities/Order";
import { PseudoLogicCart} from "../../states/PseudoLogicCart";
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
  amount: number = 1;

  constructor(private Aroute: ActivatedRoute, private pseudoLogicCart: PseudoLogicCart, private router: Router) { }

  async ngOnInit() {
    await this.getOrders();
  }

  async getOrders(){
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      if (decodToken.id)
      this.orderList = await this.pseudoLogicCart.getOrders(decodToken.id);
    }
  }

  async putOrder(id, amount, price){
    await this.pseudoLogicCart.putOrder(id, amount, price)
    console.log(amount)
  }

  async deleteOrderByID(id, productId){
    await this.pseudoLogicCart.deleteOrderByID(id, productId);
    await this.getOrders();
  }

  async placeOrder(id){
    await this.pseudoLogicCart.placeOrder(id);
    await this.getOrders();
  }

  async changeAmount(id: number | undefined, price: number | undefined){
    if(id && price)
    if(this.amount >= 1){
      await this.pseudoLogicCart.putOrder(id, this.amount, price)
    }
  }


  async sendOrderMail(userEmail) { //
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      if (decodToken.email)
        await this.pseudoLogicCart.sendOrderMail(decodToken.email);
      console.log(decodToken.email)
    }
    await this.pseudoLogicCart.sendOrderMail(userEmail)
    console.log(userEmail)
  }

}
