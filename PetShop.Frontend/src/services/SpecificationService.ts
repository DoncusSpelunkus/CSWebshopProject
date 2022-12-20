import { Injectable } from '@angular/core';
import {MatSnackBar} from "@angular/material/snack-bar";
import {catchError} from "rxjs";
import {SpecTemplates} from "../Entities/SpecTemplates";
import axios from "axios";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7143/Specs',
  headers: {
    Authorization: `Bearer ${localStorage.getItem('auth')}`
  }
})

@Injectable({
  providedIn: 'root'
})
export class SpecificationService { // Class for crud requests from the /specs route of the api

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

  async getSpecifications(){
    let httpResponse = await customAxios.get<SpecTemplates[]>('')
    return httpResponse.data;
  }

  async postSpecification(spec: any){
    let dto = {
      specName: spec.specName,
    }
    return await customAxios.post<SpecTemplates>('', dto)
  }

  async putSpecification(specification: SpecTemplates){
    let dto = {
      id: specification.id,
      specName: specification.specName,
    }
    return await customAxios.put<SpecTemplates>('/'+specification.id, dto)
  }

  async deleteSpecificationById(id: any) {
    let httpResponse = await customAxios.delete<SpecTemplates>('/'+id)
    return httpResponse.data;
  }

  async getSpecificationByID(id: number) {
    return await customAxios.get<SpecTemplates>('/' + id)
  }

}
