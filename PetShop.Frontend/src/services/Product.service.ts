import { Injectable } from '@angular/core';
import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpClient} from "@angular/common/http";
import {catchError} from "rxjs";
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
export class ProductService { // Class for crud requests from the /product route of the api
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

  async getProducts(){
    let httpResponse = await customAxios.get<Product[]>('');
    return httpResponse.data;
  }

  async postProduct(dto: any) { //
    let httpResponse = await customAxios.post<any>('',dto)
  }

  async putProduct(product: any){
    let dto = {
      name: product.name,
      price: product.price,
      description: product.description,
      imageUrl: product.imageUrl,
      mainCategoryID: product.mainCategory,
      subCategoryID: product.subCategory,
      brandID: product.brand,
      specsDescriptions: product.specsDescriptions
    }
    let httpResponse = await customAxios.put<Product>('/' + product.id, dto);
    return httpResponse.data;
  }

  async deleteProductByID(id: any){
    let httpResponse = await customAxios.delete<Product>('/' + id)
  }

  async getProductById(id: number) {
    let httpResponse = await customAxios.get<any>('/'+id)
    return httpResponse.data
  }

}
