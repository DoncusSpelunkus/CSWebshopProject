import { Component, OnInit } from '@angular/core';
import {PseudoLogicLogin} from "../../../states/PseudoLogicLogin";
import {User} from "../../../Entities/User";

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss']
})
export class UserRegistrationComponent implements OnInit {

  password: string = ''
  repeatPassword: string =''

  user: any = User;

  constructor(private loginState: PseudoLogicLogin) { }

  ngOnInit(): void {
  }

  async onRegister(){
    await this.loginState.registerUser(this.user, this.password, this.repeatPassword)
  }

}
