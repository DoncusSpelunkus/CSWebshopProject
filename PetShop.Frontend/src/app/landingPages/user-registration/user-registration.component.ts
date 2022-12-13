import { Component, OnInit } from '@angular/core';
import {LoginState} from "../../../states/LoginState";
import {Router} from "@angular/router";
import {User} from "../../../Entities/User";

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss']
})
export class UserRegistrationComponent implements OnInit {

  registerObj: any = {
    password:'',
    repeatPassword:''
  }
  user: any = User;

  constructor(private loginState: LoginState, private router: Router) { }

  ngOnInit(): void {
  }

  async onRegister(){
    await this.loginState.registerUser(this.user, this.registerObj.password, this.registerObj.repeatPassword)
  }

}
