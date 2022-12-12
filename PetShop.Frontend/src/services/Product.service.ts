import { Injectable } from '@angular/core';
import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpClient} from "@angular/common/http";
import {catchError, Observable} from "rxjs";
import {Product} from "../Entities/Product";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7143/Product',
  headers: {
    Authorization: `Bearer ${localStorage.getItem('auth')}`
  }
})

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  apiUrl = 'https://localhost:7143/Product';

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

  async getProducts(){ // calls and waits for a list of all products via the get http request of the api on the route /product
    let httpResponse = await customAxios.get<Product[]>('');
    return httpResponse.data;
  }

  async postProduct(dto: any) { //
    let httpResponse = await customAxios.post<any>('',dto)
    console.log(dto);
    console.log(httpResponse.data)
  }

  async putProduct(dto: any){
    let httpResponse = await customAxios.put<Product>('', dto);
    return httpResponse.data;
  }

  async deleteProductByID(id: any){
    let httpResponse = await customAxios.delete<Product>('/' + id)
  }

  async getProductById(id: number) {
    let httpResponse = await customAxios.get<Product>('/'+id)
    return httpResponse.data
  }

}
