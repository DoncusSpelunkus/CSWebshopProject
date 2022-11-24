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
        this.matSnackbar.open("Great success")
      }
      return response;
    }, rejected => {
      if(rejected.response.status>=400 && rejected.response.status <= 500) {
        matSnackbar.open(rejected.response.data);
      }
        catchError(rejected);
    }
    )
  }

  async GetBoxes(){
    const httpResponse = await customAxios.get<any>('Box');
    return httpResponse.data;
  }

  async createBox(dto: { length: number; width: number; height: number }) {
    const httpResult = await customAxios.post('box', dto);
    return httpResult.data;
  }

  async deleteBox(manFacId: any) {
    const httpResult = await customAxios.delete('box/' + manFacId);
    return httpResult.data;
  }
}
