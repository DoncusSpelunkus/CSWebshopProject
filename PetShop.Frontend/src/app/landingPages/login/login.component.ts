import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {LoginState} from "../../../states/LoginState";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  user: any;

  loginObj: any = {
    email: '',
    password:''
  };
  constructor(private router: Router, private loginState: LoginState) { }

  ngOnInit(): void {
  }

  async onLogin(){
    await this.loginState.onLoginCall(this.loginObj.email, this.loginObj.password);
    let thisRole = this.loginState.getTokenRole();
    if(thisRole === "Admin"){
      await this.router.navigateByUrl("admin")
    }
    else if (thisRole === "User"){
      await this.router.navigateByUrl("user")
    }
  }

}
