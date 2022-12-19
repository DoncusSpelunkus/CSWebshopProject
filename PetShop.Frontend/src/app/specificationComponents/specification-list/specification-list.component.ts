import {Component, OnInit} from '@angular/core';
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";

@Component({
  selector: 'app-specification-list',
  templateUrl: './specification-list.component.html',
  styleUrls: ['./specification-list.component.scss']
})
export class SpecificationListComponent implements OnInit {
  specificationList: SpecTemplates[] = [];

  constructor(private psuedoLogicAdmin: PsuedoLogicAdmin) {

  }
s
  async ngOnInit() {
    this.specificationList = await this.psuedoLogicAdmin.getSpecifications()
  }

  async updateList(){
    await this.ngOnInit()
  }


}
