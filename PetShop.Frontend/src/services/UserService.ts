import { Injectable } from '@angular/core';
import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpClient} from "@angular/common/http";
import {catchError} from "rxjs";
import {User} from "../Entities/User";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7143/User',
  headers: {
    Authorization: `Bearer ${localStorage.getItem('auth')}`
  }
})

@Injectable({
  providedIn: 'root'
})
export class UserService { // Class for crud requests from the /user route of the api
  apiUrl = 'https://localhost:7143/User';

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

  async postUser(dto: any) { //
    await customAxios.post<any>('/register', dto)
  }

  async putUser(id, dto){
    console.log(dto)
    let httpResponse = await customAxios.put<User>('update?userID=' + id + '&currentPassword=' + dto.password, dto);
  }

  async deleteUserById(id: any){
    await customAxios.delete<User>('/' + id)
  }

  async getUsers(){
    let httpResponse = await customAxios.get<User>('')
    return httpResponse.data
  }


}
