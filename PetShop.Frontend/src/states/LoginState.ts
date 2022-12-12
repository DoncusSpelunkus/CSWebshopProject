import {Injectable} from "@angular/core";
import {LoginService} from "../services/LoginService";

@Injectable({ providedIn: 'root' })

export class LoginState{
  constructor(private loginService: LoginService) {
  }

  async onLoginCall(email: string, password: string){
    let data = await this.loginService.onLoginCall(email,password);
    localStorage.setItem('auth', data);

  }

  async registerUser(email: string, password: string, repeatPassword: string){
    if(password === repeatPassword){
      let data = await this.loginService.registerUser(email, password);
      return data;
    }
  }
}
