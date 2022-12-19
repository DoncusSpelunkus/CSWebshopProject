import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import { Router} from "@angular/router";
import { Product } from "../../../Entities/Product";
import { SpecTemplates } from "../../../Entities/SpecTemplates";
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";
import {Category} from "../../../Entities/Category";

@Component({
  selector: 'app-product-creation',
  templateUrl: './product-creation.component.html',
  styleUrls: ['./product-creation.component.scss']
})
export class ProductCreationComponent implements OnInit{
  newProduct: any = new Product();
  specList: CurrentSpecs[] = [];
  specNames: SpecTemplates[] = [];
  specDesc: {specsId: number, description: string}[] = [];
  mainCatList: Category[] = [];
  subCatList: Category[] = [];
  brandCatList: Category[] = [];
  sname: number = 0;
  sdesc: string = '';
  productDeleteId: number = 0;
  selected: any = SpecTemplates;

  @ViewChild('child') child;

  @Output()
  change: EventEmitter<any> = new EventEmitter<any>();

  constructor(public router: Router, private adminState: PsuedoLogicAdmin){
  }

  async ngOnInit() {
    this.specNames = await this.adminState.getSpecifications();
    this.mainCatList = await this.adminState.getCategories("MainCat")
    this.subCatList = await this.adminState.getCategories("SubCat")
    this.brandCatList = await this.adminState.getCategories("Brand")
  }

  async postProduct() {
    await this.adminState.postProduct(this.newProduct)
    this.change.emit()
  }


  async deleteProduct(id){
    await this.adminState.deleteProductById(id);
    this.change.emit()
  }

  onDelete(event: number): void{ // Delete the spec with the id from the mat-card selected in the child component
    this.specList.splice(this.specList.findIndex(a => a.specsId === event) , 1)
    this.child.updateNow(this.specList);
    this.change.emit();
  }

  addTooSpecList(desc: string){
    let newSpec = new CurrentSpecs();
    newSpec.specsId = this.selected.id;
    newSpec.description = desc;
    newSpec.specName = this.selected.specName;
    this.specDesc.push({specsId: this.selected.id, description: desc}) // attachs value on an Array that mirrors the requested field in the post method
    this.newProduct.specsDescriptions = this.specDesc;
    this.specList.push(newSpec)
    this.child.updateNow(this.specList); // signals the specification list to refresh
  }
}
