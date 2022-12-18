import { Injectable } from '@angular/core';
import { Order} from "../Entities/Order";
import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpClient} from "@angular/common/http";
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

  constructor(private matSnackbar: MatSnackBar, private http: HttpClient) {
    customAxios.interceptors.response.use(
      response => {
        if(response.status == 201) {
          this.matSnackbar.open("Great success", "x", {duration: 500})
        }
        return response;
      }, rejected => {
        if(rejected.response.status>=400 && rejected.response.status <= 500) {
          matSnackbar.open(rejected.response.data, "x", {duration: 500});
        }
        catchError(rejected);
      }
    )
  }

  async getOrders(id: number){
      let httpResponse = await customAxios.get<Order[]>('?userId=' + id);
      console.log(httpResponse.data)
      return httpResponse.data;
  }

  async getOrderById(id: number) {
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      console.log(decodToken)
    let httpResponse = await customAxios.get<any>('/OrderHistory' + decodToken.id)
      return httpResponse.data;
    }
  }

  async postOrder(dto: any, id: number) { //
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      let httpResponse = await customAxios.post<any>('/' + decodToken.id, dto)
    }
  }

  async putOrder(dto: any, id: number){
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodToken = jwtDecode(localToken) as User;
      let httpResponse = await customAxios.post<any>('/' + decodToken.id, dto)
    }
  }

  async deleteOrderByID(id: any){
    let httpResponse = await customAxios.delete<Order>('/' + id)
  }

  async placeOrder(id: number) { //
    let httpResponse = await customAxios.post<any>('/OrderHistory/' + id)
    return httpResponse.data;
  }

  async SendOrderMail(userEmail: String) { //
    let httpResponse = await customAxios.post<any>('/sendMail/' + userEmail)
    return httpResponse.data;
  }

}
