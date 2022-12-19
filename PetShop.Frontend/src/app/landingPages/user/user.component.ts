import { Component, OnInit } from '@angular/core';
import {CartService} from "../../../services/cart.service";
import {PseudoLogicCart} from "../../../states/PseudoLogicCart";
import {Order} from "../../../Entities/Order";
import {ActivatedRoute} from "@angular/router";
import jwtDecode from "jwt-decode";
import {User} from "../../../Entities/User";
import {Product} from "../../../Entities/Product";
import {OrderProduct} from "../../../Entities/OrderProduct";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  product: Product[] = [];
  order: any = Order;
  orderList: Order[] = [];
  orderProduct: any = OrderProduct;
  orderProductList: OrderProduct[] = [];
  interMediateList: Order[] = [];

  constructor(private Aroute: ActivatedRoute, private cartService: CartService, private cartState: PseudoLogicCart) { }

  async ngOnInit() {
    await this.orderSort();
  }

  async getOrderById() {
    console.log(this.orderList)
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      if (decodToken.id)
        this.orderList = await this.cartState.getOrderById(decodToken.id);
    }
    console.log(this.orderList)
  }

  async orderSort(){
    await this.getOrderById();
    let orderProduct = new OrderProduct()
    let number = this.orderList[0].orderId
    let dateOfOrder: string | undefined;
    this.orderList.forEach((o) => {
      dateOfOrder = o.dateOfOrder?.split("T")[0];
      let orderProduct = new OrderProduct()
      if(o.orderId === number){
        this.interMediateList.push(o)
      }
      if(o.orderId != number){
        orderProduct.uniqueId = number;
        orderProduct.listOfUniqueId = this.interMediateList
        orderProduct.time = o.dateOfOrder?.split("T")[0];
        this.orderProductList.push(orderProduct)
        this.interMediateList = [];
        this.interMediateList.push(o)
        number = o.orderId;
      }});
      orderProduct.uniqueId = number;
      orderProduct.listOfUniqueId = this.interMediateList
      orderProduct.time = dateOfOrder;
      this.orderProductList.push(orderProduct)
      this.interMediateList = [];
    }
}
