import { Component, OnInit } from '@angular/core';
import {Specification} from "../../../Entities/specification";
import {SpecificationService} from "../../../services/SpecificationService";

@Component({
  selector: 'app-specification-creation',
  templateUrl: './specification-creation.component.html',
  styleUrls: ['./specification-creation.component.scss']
})
export class SpecificationCreationComponent implements OnInit {

specification: any;
specificationDeletionId: number;

  constructor(private specificationService: SpecificationService) {
    this.specification = new Specification;
    this.specificationDeletionId = 0;
  }

  ngOnInit(): void {
  }

  createSpecification(){
    this.specificationService.addSpecification(this.specification).subscribe();
  }

  deleteSpecification(){
    this.specificationService.deleteSpecificationById(this.specificationDeletionId).subscribe()
  }

}
