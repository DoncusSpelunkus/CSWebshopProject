import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {User} from "../../../Entities/User";
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";

@Component({
  selector: 'app-user-admin-control',
  templateUrl: './user-admin-control.component.html',
  styleUrls: ['./user-admin-control.component.scss']
})
export class UserAdminControlComponent implements OnInit {


  newUser: any = User;
  deleteId: number = 0;
  password: string = '';
  @Output()
  change: EventEmitter<any> = new EventEmitter<any>();

  constructor(private psuedoLogicAdmin: PsuedoLogicAdmin) { }

  ngOnInit(): void {
  }

  async postUser(){
    await this.psuedoLogicAdmin.postUser(this.newUser, this.password);
    this.change.emit();
  }

  async deleteUser(){
    await this.psuedoLogicAdmin.deleteUserById(this.newUser.id)
    this.change.emit();
  }


}
