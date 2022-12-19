import {Component, EventEmitter, OnInit, Output} from '@angular/core';
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

  @Output()
  change: EventEmitter<any> = new EventEmitter<any>();

  constructor(private adminState: AdminState) {
    this.specification = new SpecTemplates;
    this.specificationDeletionId = 0;
  }

  ngOnInit(): void {
  }

  async postSpecification(){
    await this.adminState.postSpecification(this.specification);
    this.change.emit()
  }

  async deleteSpecification(){
    await this.adminState.deleteSpecificationById(this.specificationDeletionId);
    this.change.emit()
  }

}
