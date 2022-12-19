import {Injectable} from "@angular/core";
import {LoginService} from "../services/LoginService";
import jwtDecode from "jwt-decode";
import {User} from "../Entities/User";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";

@Injectable({ providedIn: 'root' })

export class PseudoLogicLogin {

  user: any;

  constructor(private loginService: LoginService, private matSnackbar: MatSnackBar, private router: Router) {
  }

  async onLoginCall(email: string, password: string){
    let dto = {
      email: email,
      password: password
    }
    let data = await this.loginService.onLoginCall(dto);
    localStorage.setItem('auth', data);
  }

  async registerUser(user: User, password: string, repeatPassword: string){
    if(password != ''){
      if(password === repeatPassword){
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
        let status = await this.loginService.registerUser(dto)
        if(status == 201){
          await this.matSnackbar.open("Great success", 'x', {duration:500})
          await this.router.navigateByUrl('')
        }
      }
      else
      this.matSnackbar.open("Passwords are not the same")
    }
    else
    this.matSnackbar.open("Password can not be empty")
  }

  getTokenRole(){
    let userProperties = localStorage.getItem('auth')
    console.log(userProperties)
    if (userProperties != null) {
      let decodedToken = jwtDecode(userProperties) as User;
      if (decodedToken.type != null) {
        return decodedToken.type;
    }
      else return undefined;
  }
    else return undefined;
  }
}
