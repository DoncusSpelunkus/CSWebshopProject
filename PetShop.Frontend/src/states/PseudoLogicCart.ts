import {Injectable} from "@angular/core";
import {CartService} from "../services/cart.service";


@Injectable({ providedIn: 'root' })

export class PseudoLogicCart { // State class for data manipulation

  constructor(private cartService: CartService) {
  }

  async getOrders(id){ // Get Cart
      return await this.cartService.getOrders(id);
  }

  async getOrderById(id){ // Gets all previous orders.
    return await this.cartService.getOrderById(id);
  }

  async postOrder(id: number, price: number) { // Add to cart
    let dto = {
      productId: id,
      amount: 1,
      price: price
    }
    await this.cartService.postOrder(dto, dto.productId);
  }

  async putOrder(id: number, amount: number, price: number){ // Edit cart
    let dto = {
      productId: id,
      amount: amount,
      price: price
    }
    await this.cartService.putOrder(dto);
  }

  async deleteOrderByID(id: any, productId: any) { // Remove from cart
    return this.cartService.deleteOrderByID(id, productId);
  }

  async placeOrder(id: any) { // Checkout from cart
    return this.cartService.placeOrder(id);
  }

  async sendOrderMail(userEmail: String) { //
    return this.cartService.sendOrderMail(userEmail);
  }
}
