import { Component, OnInit } from '@angular/core';
import {SpecificationService} from "../../../services/SpecificationService";
import {Specification} from "../../../Entities/specification";

@Component({
  selector: 'app-specification-list',
  templateUrl: './specification-list.component.html',
  styleUrls: ['./specification-list.component.scss']
})
export class SpecificationListComponent implements OnInit {
  specificationList: Specification[] = [];

  constructor(private specificationService: SpecificationService) {

  }
s
  ngOnInit(): void {
    this.specificationService.getSpecifications().subscribe(products => this.specificationList = products)
  }

}
