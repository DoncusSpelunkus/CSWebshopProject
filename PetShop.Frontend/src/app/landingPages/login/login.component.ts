import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {PseudoLogicLogin} from "../../../states/PseudoLogicLogin";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  user: any;

  email: string = ''
  password: string = ''

  constructor(private router: Router, private pseudoLogicLogin: PseudoLogicLogin) { }

  ngOnInit(): void {
  }

  async onLogin(){
    await this.pseudoLogicLogin.onLoginCall(this.email, this.password);
  }

}
