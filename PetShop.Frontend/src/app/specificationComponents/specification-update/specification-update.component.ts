import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";

@Component({
  selector: 'app-specification-update',
  templateUrl: './specification-update.component.html',
  styleUrls: ['./specification-update.component.scss']
})
export class SpecificationUpdateComponent implements OnInit {

specification: any;

  constructor(private Aroute: ActivatedRoute, private psuedoLogicAdmin: PsuedoLogicAdmin, public route: Router) {
    this.specification = SpecTemplates;
  }

  async ngOnInit() {
    const id = Number(this.Aroute.snapshot.paramMap.get('id'))
    this.specification = await this.psuedoLogicAdmin.getSpecificationById(id);
  }

  async updateSpec(){
    let data = await this.psuedoLogicAdmin.putSpecification(this.specification);
    if(data != undefined){
      await this.route.navigateByUrl("admin")
    }
  }

}
