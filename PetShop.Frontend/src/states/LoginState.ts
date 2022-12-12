import {Injectable} from "@angular/core";
import {LoginService} from "../services/LoginService";

@Injectable({ providedIn: 'root' })

export class LoginState{
  constructor(private loginService: LoginService) {
  }

  async onLoginCall(username: string, password: string){
    let data = await this.loginService.onLoginCall(username,password);
    localStorage.setItem('auth', data);

  }

  async registerUser(username: string, password: string, repeatPassword: string){
    if(password === repeatPassword){
      let data = await this.loginService.registerUser(username, password);
      return data;
    }
  }
}
