import {Component, OnInit, ViewChild} from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { Product } from "../../../Entities/Product";
import { Router } from "@angular/router";
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";
import {AdminState} from "../../../states/AdminState";
import {Category} from "../../../Entities/Category";

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit { // This component tend to the editing of products
  product: any = Product;
  specList: CurrentSpecs[] = [];
  specNames: SpecTemplates[] = [];
  specDesc: {specsId: number, description: string}[] = [];
  mainCatList: Category[] = [];
  subCatList: Category[] = [];
  brandCatList: Category[] = [];
  sdesc: string = '';
  selected: number = 0;
  spec: any;
  specId: number = 0;

  @ViewChild('child') child;


  constructor(private Aroute: ActivatedRoute, private adminState: AdminState, public route: Router) {
  }

  async ngOnInit() {
    const id = Number(this.Aroute.snapshot.paramMap.get(`id`))
    this.product = await this.adminState.getProductById(id);
    this.specNames = await this.adminState.getSpecifications();
    this.mainCatList = await this.adminState.getCategories("MainCat")
    this.subCatList = await this.adminState.getCategories("SubCat")
    this.brandCatList = await this.adminState.getCategories("Brand")
    this.specDesc = this.product.specsDescriptions;
    this.specList = await this.adminState.makeCurrentSpecList(id)
    this.child.updateNow(this.specList)
  }

  async updateProduct(){
    console.log(this.product)
    await this.adminState.putProduct(this.product);
  }

  addTooSpecList(spec: number, desc: string){
    let newSpec = new CurrentSpecs();
    newSpec.specsId = spec;
    newSpec.description = desc;
    this.specDesc.push({specsId: spec, description: desc}) // attachs value on an Array that mirrors the requested field in the post method
    this.specList.push(newSpec)
    this.product.specsDescriptions = this.specDesc;
    console.log(this.product.specsDescriptions)
    this.child.updateNow(this.specList); // signals the specification list to refresh
  }

  onDelete(event: number): void{ // Delete the spec with the id from the mat-card selected in the child component
    this.specList.splice(this.specList.findIndex(a => a.specsId === event) , 1)
    this.child.updateNow(this.specList);
  }
}
