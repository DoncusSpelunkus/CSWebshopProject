import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpClient} from "@angular/common/http";
import {catchError} from "rxjs";
import {Injectable} from "@angular/core";
import {User} from "../Entities/User";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7143/User/',
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
          matSnackbar.open("Incorrect username or password", "x", {duration: 500});
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
    let httpResponse = await customAxios.post<any>('login',dto);
    return httpResponse.data;
  }

  async registerUser(user: User, password: string) {
    let dto = {
      name: user.fullName,
      password: password,
      email: user.email,
      address: user.address,
      city: user.city,
      zip: user.zip,
      phone: user.phone,
      type: 1
    }
    let httpResponse = await customAxios.post<any>('register', dto);
    return httpResponse.status;
  }
}
