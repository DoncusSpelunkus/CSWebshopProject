import {Component, OnInit, ViewChild} from '@angular/core';
import { Router} from "@angular/router";
import { Product } from "../../../Entities/Product";
import { SpecTemplates } from "../../../Entities/SpecTemplates";
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";
import {AdminState} from "../../../states/AdminState";

@Component({
  selector: 'app-product-creation',
  templateUrl: './product-creation.component.html',
  styleUrls: ['./product-creation.component.scss']
})
export class ProductCreationComponent implements OnInit{
  newProduct: any;
  specList: CurrentSpecs[] = [];
  specNames: SpecTemplates[] = [];
  specDesc: {specsId: number, description: string}[] = [];
  sname: number;
  sdesc: string;
  productDeleteId: number;
  selected: number = 0;

  @ViewChild('child') child;

  constructor(public router: Router, private adminState: AdminState){
    this.newProduct = new Product;
    this.sname = 0;
    this.sdesc = '';
    this.productDeleteId = 0;
  }

  async ngOnInit() {
    this.specNames = await this.adminState.getSpecifications();
  }

  async postProduct() {
    await this.adminState.postProduct(this.newProduct)
  }


  deleteProduct(id){
    this.adminState.deleteProductById(id).then(r => this.updateList());
  }

  updateList(){

  }

  onDelete(event: number): void{ // Delete the spec with the id from the mat-card selected in the child component
    this.specList.splice(this.specList.findIndex(a => a.specsId === event) , 1)
    this.child.updateNow(this.specList);
  }

  addTooSpecList(spec: number, desc: string){
    let newSpec = new CurrentSpecs();
    newSpec.specsId = spec;
    newSpec.description = desc;
    this.specDesc.push({specsId: spec, description: desc}) // attachs value on an Array that mirrors the requested field in the post method
    this.newProduct.specsDescriptions = this.specDesc;
    this.specList.push(newSpec)
    this.child.updateNow(this.specList); // signals the specification list to refresh
  }

  consoleLog(){
    console.log(this.newProduct)
  }
}
