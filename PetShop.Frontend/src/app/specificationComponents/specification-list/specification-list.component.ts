import {Component, OnInit} from '@angular/core';
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {AdminState} from "../../../states/AdminState";

@Component({
  selector: 'app-specification-list',
  templateUrl: './specification-list.component.html',
  styleUrls: ['./specification-list.component.scss']
})
export class SpecificationListComponent implements OnInit {
  specificationList: SpecTemplates[] = [];

  constructor(private adminState: AdminState) {

  }
s
  async ngOnInit() {
    await this.getList();
  }

  async updateList(){
    await this.ngOnInit()
  }

  async getList(){
    this.specificationList = await this.adminState.getSpecifications()
  }

}
