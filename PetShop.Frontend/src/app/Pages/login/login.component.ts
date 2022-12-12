import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {LoginState} from "../../../states/LoginState";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginObj: any = {
    email: '',
    password:''
  };
  constructor(private router: Router, private loginState: LoginState) { }

  ngOnInit(): void {
  }

  onLogin(){
    this.loginState.onLoginCall(this.loginObj.email, this.loginObj.password)
    this.router.navigateByUrl("admin")
  }

}
