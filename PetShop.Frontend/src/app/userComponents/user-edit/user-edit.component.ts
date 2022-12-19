import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {User} from "../../../Entities/User";
import jwtDecode from "jwt-decode";
import {PseudoLogicUser} from "../../../states/PseudoLogicUser";
import {PseudoLogicLogin} from "../../../states/PseudoLogicLogin";

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  user: any;
  anything: string = '';

  constructor(private Aroute: ActivatedRoute, private userState: PseudoLogicUser, public route: Router, private loginState: PseudoLogicLogin) {
    this.user = User;
  }

  async ngOnInit() {
    let localToken = localStorage.getItem('auth')
    if(localToken) {
      let decode = jwtDecode(localToken) as User;
      this.user = decode
      if(decode.name)
      this.anything = decode.name;

    }

  }

  async postUser() {
    await this.userState.postUser(this.user)
  }

  async updateUser(password){
    await this.userState.putUser(this.user, password, this.anything);
    localStorage.clear()
    await this.loginState.onLoginCall(this.user.email, password)
    await this.ngOnInit()
  }

  deleteProduct(id){
    this.userState.deleteUserById(id);
  }

  async validatePassword(password){
    if(password === this.user.password){
      await this.updateUser(password)
    }
}

}


