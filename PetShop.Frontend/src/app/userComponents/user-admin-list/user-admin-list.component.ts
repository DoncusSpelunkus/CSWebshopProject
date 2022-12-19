import { Component, OnInit } from '@angular/core';
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";

@Component({
  selector: 'app-user-admin-list',
  templateUrl: './user-admin-list.component.html',
  styleUrls: ['./user-admin-list.component.scss']
})
export class UserAdminListComponent implements OnInit {

  userList: any;

  constructor(private psuedoLogicAdmin: PsuedoLogicAdmin) { }

  async ngOnInit() {
    this.userList = await this.psuedoLogicAdmin.getUserList()
  }

  async updateList(){
    await this.ngOnInit()
  }

  async userDelete(id: number){
    this.userList.filter(x => x.id === id);
    await this.psuedoLogicAdmin.deleteUserById(id)
    await this.updateList()
  }
}
