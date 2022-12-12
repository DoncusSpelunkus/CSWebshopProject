import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpClient} from "@angular/common/http";
import {catchError} from "rxjs";
import {Injectable} from "@angular/core";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7143/User/login',
  headers: {
    Authorization: `Bearer ${localStorage.getItem('auth')}`
  }
})

@Injectable({
  providedIn: 'root'
})
export class LoginService{

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

  async onLoginCall(email: string, password: string){
    let dto = {
      email: email,
      password: password
    }
    let httpResponse = await customAxios.post<any>('',dto);
    console.log(httpResponse)
    return httpResponse.data;
  }

  async registerUser(email: string, password: string) {
    let dto = {
      email: email,
      password: password
    }
    let httpResponse = await customAxios.post<any>('', dto);
    return httpResponse.data;

  }
}
