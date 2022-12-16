import { Injectable } from '@angular/core';
import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {catchError} from "rxjs";
import {Category} from "../Entities/Category";


export const customAxios = axios.create({
  baseURL: 'https://localhost:7143/',
  headers: {
    Authorization: `Bearer ${localStorage.getItem('auth')}`
  }
})

@Injectable({
  providedIn: 'root'
})
export class CategoryService { // Class for crud requests from the all the category routes of the api

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

  async getCategories(path: string){
    let httpResponse = await customAxios.get<Category[]>(path);
    return httpResponse.data;

  }

  async postCategory(dto: any, path: string) { //
    await customAxios.post<any>(path,dto)
  }

  async putCategory(cat: any, path: string){
    let dto = {
      name: cat.name
    }
    return await customAxios.put<Category>(path + "/" + cat.id, dto);
  }

  async deleteCategoryByID(id: any, path: string){
    let httpResponse = await customAxios.delete<Category>(path + '/' + id)
  }

  async getCategoryById(id: number, path: string) {
    let httpResponse = await customAxios.get<Category>(path+'/'+id)
    return httpResponse.data
  }

}

