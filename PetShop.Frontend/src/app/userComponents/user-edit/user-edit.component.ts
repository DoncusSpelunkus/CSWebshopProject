import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {User} from "../../../Entities/User";
import jwtDecode from "jwt-decode";
import {UserState} from "../../../states/UserState";

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  user: any;

  constructor(private Aroute: ActivatedRoute, private userState: UserState, public route: Router) {
    this.user = User;
  }

  async ngOnInit() {
    let localToken = localStorage.getItem('auth')
    if(localToken) {
      let decode = jwtDecode(localToken) as User;
      this.user = decode
    }

  }

  async postUser() {
    await this.userState.postUser(this.user)
  }


  updateUser(){
    this.userState.putUser(this.user);
  }

  deleteProduct(id){
    this.userState.deleteUserById(id);
  }

}


