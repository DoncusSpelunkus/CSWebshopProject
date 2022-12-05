import { Injectable } from '@angular/core';
import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpClient} from "@angular/common/http";
import {catchError, Observable} from "rxjs";
import {Product} from "../Entities/Product";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7143'
})

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  apiUrl = 'https://localhost:7143/shop';

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

  getProductStatus(): Observable<[Product]>{
    return this.http.get<[Product]>(this.apiUrl)

  }

  addProduct(product: Product): Observable<Product>{
    let dto = {
      name: product.name,
      price: product.price,
      description: product.description,
      imageUrl: product.imageUrl,
      rating: product.rating,
      mainCategory: product.mainCategory,
      subCategory: product.subCategory,
      brand: product.brand
    }
    return this.http.post<Product>(this.apiUrl, dto);
  }

  UpdateProduct(product: Product): Observable<Product>{
    let dto = {
      id: product.id,
      name: product.name,
      price: product.price,
      description: product.description,
      imageUrl: product.imageUrl,
      rating: product.rating,
      mainCategory: product.mainCategory,
      subCategory: product.subCategory,
      brand: product.brand
    }
    console.log(this.apiUrl+'/'+product.id, Product)
    return this.http.put<Product>(this.apiUrl+'/'+product.id, dto)
  }

  DeleteProductByID(id: any): Observable<Product> {
    return this.http.delete<Product>(this.apiUrl+'/' + id)
  }

  GetProductByID(id: number): Observable<Product> {
    return this.http.get<Product>(this.apiUrl+'/' + id)
}

}
