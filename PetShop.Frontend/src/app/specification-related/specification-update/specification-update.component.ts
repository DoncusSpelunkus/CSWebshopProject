import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {AdminState} from "../../../states/AdminState";

@Component({
  selector: 'app-specification-update',
  templateUrl: './specification-update.component.html',
  styleUrls: ['./specification-update.component.scss']
})
export class SpecificationUpdateComponent implements OnInit {

specification: any;

  constructor(private Aroute: ActivatedRoute, private adminState: AdminState, public route: Router) {
    this.specification = SpecTemplates;
  }

  ngOnInit(): void {
    const id = Number(this.Aroute.snapshot.paramMap.get('id'))
    this.specification = this.adminState.getSpecificationById(id);
  }

  updateSpec(){
    this.adminState.putSpecification(this.specification);
  }

}
