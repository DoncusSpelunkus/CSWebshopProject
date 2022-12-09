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
}
