import { Component, OnInit } from '@angular/core';
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {AdminState} from "../../../states/AdminState";

@Component({
  selector: 'app-specification-creation',
  templateUrl: './specification-creation.component.html',
  styleUrls: ['./specification-creation.component.scss']
})
export class SpecificationCreationComponent implements OnInit {

specification: any;
specificationDeletionId: number;

  constructor(private adminState: AdminState) {
    this.specification = new SpecTemplates;
    this.specificationDeletionId = 0;
  }

  ngOnInit(): void {
  }

  postSpecification(){
    this.adminState.postSpecification(this.specification);
  }

  deleteSpecification(){
    this.adminState.deleteSpecificationById(this.specificationDeletionId);
  }

}
