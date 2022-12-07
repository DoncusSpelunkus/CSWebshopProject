import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {SpecificationService} from "../../../services/SpecificationService";
import {Specification} from "../../../Entities/specification";

@Component({
  selector: 'app-specification-update',
  templateUrl: './specification-update.component.html',
  styleUrls: ['./specification-update.component.scss']
})
export class SpecificationUpdateComponent implements OnInit {

specification: any;

  constructor(private Aroute: ActivatedRoute, private specificationService: SpecificationService, public route: Router) {
    this.specification = Specification;
  }

  ngOnInit(): void {
    const id = Number(this.Aroute.snapshot.paramMap.get('id'))
    this.specificationService.getSpecificationByID(id).subscribe(specReceived => {this.specification = specReceived})
  }

  updateSpec(){
    this.specificationService.updateSpecification(this.specification)
      .subscribe(() =>{
        this.route.navigateByUrl('/admin')
      });
  }

}
