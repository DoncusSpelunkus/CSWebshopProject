import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";

@Component({
  selector: 'app-specification-creation',
  templateUrl: './specification-creation.component.html',
  styleUrls: ['./specification-creation.component.scss']
})
export class SpecificationCreationComponent implements OnInit {

specification: any;
specificationDeletionId: number;

  @Output()
  change: EventEmitter<any> = new EventEmitter<any>();

  constructor(private psuedoLogicAdmin: PsuedoLogicAdmin) {
    this.specification = new SpecTemplates;
    this.specificationDeletionId = 0;
  }

  ngOnInit(): void {
  }

  async postSpecification(){
    await this.psuedoLogicAdmin.postSpecification(this.specification);
    this.change.emit()
  }

  async deleteSpecification(){
    await this.psuedoLogicAdmin.deleteSpecificationById(this.specificationDeletionId);
    this.change.emit()
  }

}
