import { Component, OnInit } from '@angular/core';
import {AdminState} from "../../../states/AdminState";

@Component({
  selector: 'app-user-admin-list',
  templateUrl: './user-admin-list.component.html',
  styleUrls: ['./user-admin-list.component.scss']
})
export class UserAdminListComponent implements OnInit {

  userList: any;

  constructor(private adminState: AdminState) { }

  async ngOnInit() {
    this.userList = await this.adminState.getUserList()
  }

  async userDelete(id: number){
    this.userList.filter(x => x.id === id);
    await this.adminState.deleteUserById(id)
  }
}
