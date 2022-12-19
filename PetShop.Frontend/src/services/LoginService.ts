import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
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
export class LoginService{ // Class for crud requests from the /user/** route of the api (login and registration)

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

  async onLoginCall(dto: any){
    let httpResponse = await customAxios.post<any>('login',dto);
    return httpResponse.data;
  }

  async registerUser(dto: any) {
    let httpResponse = await customAxios.post<any>('register', dto);
    return httpResponse.status;
  }
}
