import {Component, OnInit, ViewChild} from '@angular/core';
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {ActivatedRoute, Router} from "@angular/router";
import {AdminState} from "../../../states/AdminState";
import {Product} from "../../../Entities/Product";
import {User} from "../../../Entities/User";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  user: any;

  constructor(private Aroute: ActivatedRoute, private userState: AdminState, public route: Router) {
    this.user = User;
  }

  async ngOnInit() {
    let localToken = localStorage.getItem('auth')
    if(localToken) {
      let decode = jwtDecode(localToken) as User;
      this.user = decode
    }
  }

  updateUser(){
    this.userState.putProduct(this.user);
  }

  onDelete(event: number): void{

  }

}


