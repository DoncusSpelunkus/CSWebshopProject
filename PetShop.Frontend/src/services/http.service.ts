import { Injectable } from '@angular/core';
import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {catchError} from "rxjs";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7143'
})

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private matSnackbar: MatSnackBar) {
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

  async GetProduct(){
    const httpResponse = await customAxios.get<any>('Shop');
    return httpResponse.data;
  }

  async CreateProduct(dtoi){
    let dto = {
      name: dtoi.pname,
      price: dtoi.price,
      description: dtoi.description,
      imageUrl: dtoi.imageUrl,
      rating: dtoi.rating,
      mainCategory: dtoi.mainCategory,
      subCategory: dtoi.subCategory,
      brand: dtoi.brand
    }
    const httpResult = await customAxios.post('Shop', dto);
    return httpResult.data;
  }

  async DeleteProductByID(id: any) {
    const httpResult = await customAxios.delete('Shop/' + id);
    return httpResult.data;
  }
}
