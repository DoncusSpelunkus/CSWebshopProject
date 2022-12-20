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

  async onLoginCall(email: string, password: string){ // converts the login details to dto object and navigates the user after login
    let dto = {
      email: email,
      password: password
    }
    let data = await this.loginService.onLoginCall(dto);
    localStorage.setItem('auth', data);
    let thisRole = this.getTokenRole();
    if(thisRole === "Admin"){
      await this.router.navigateByUrl("admin")
    }
    else if (thisRole === "User"){
      await this.router.navigateByUrl("user")
    }
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
          await this.matSnackbar.open("Great success", 'x', {duration:1000})
          await this.router.navigateByUrl('')
        }
      }
      else
      this.matSnackbar.open("Passwords are not the same")
    }
    else
    this.matSnackbar.open("Password can not be empty")
  }

  getTokenRole(){ // gets the tokens user type
    let userProperties = localStorage.getItem('auth')
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
