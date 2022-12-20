import { Injectable } from '@angular/core';
import { Order} from "../Entities/Order";
import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {catchError} from "rxjs";
import jwtDecode from "jwt-decode";
import {User} from "../Entities/User";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7143/Order',
  headers: {
    Authorization: `Bearer ${localStorage.getItem('auth')}`
  }
})

@Injectable({
  providedIn: 'root'
})

export class CartService {

  constructor(private matSnackbar: MatSnackBar) {
    customAxios.interceptors.response.use(
      response => {
        if(response.status == 201) {
          this.matSnackbar.open("Great success", "x", {duration: 1000})
        }
        return response;
      }, rejected => {
        if(rejected.response.status>=400 && rejected.response.status <= 500) {
          matSnackbar.open(rejected.response.data, "x", {duration: 1000});
        }
        catchError(rejected);
      }
    )
  }

  async getOrders(id: number){
    let httpResponse = await customAxios.get<Order[]>('?userId=' + id);
    return httpResponse.data;
  }

  async getOrderById(id: number) {
    let httpResponse = await customAxios.get<Order[]>('/OrderHistory?userId=' + id)
    return httpResponse.data;
  }

  async postOrder(dto: any, id: number) {
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      await customAxios.post<any>('/' + decodToken.id, dto)
    }
  }

  async putOrder(dto: any){
    let localToken = localStorage.getItem('auth');
    console.log(dto)
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      await customAxios.put<any>('/' + decodToken.id, dto)
    }
  }

  async deleteOrderByID(id: any, productId: any){
    await customAxios.delete<Order>('/' + id + '?productId=' + productId)
  }

  async placeOrder() {
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      await customAxios.post<any>('/OrderHistory,' + decodToken.id)
    }
  }

  async sendOrderMail(userEmail: String) {
    await customAxios.post<any>('/sendEmail?userEmail=' + userEmail)
  }

}
