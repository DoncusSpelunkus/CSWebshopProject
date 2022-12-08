import { Component, OnInit } from '@angular/core';
import {SpecificationService} from "../../../services/SpecificationService";
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
    let specs = await this.adminState.getSpecifications();
    this.specificationList = specs;
  }

}
