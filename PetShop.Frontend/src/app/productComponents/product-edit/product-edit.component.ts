import {Component, OnInit, ViewChild} from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { Product } from "../../../Entities/Product";
import { Router } from "@angular/router";
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";
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


  constructor(private Aroute: ActivatedRoute, private psuedoLogicAdmin: PsuedoLogicAdmin, public route: Router) {
  }

  async ngOnInit() {
    const id = Number(this.Aroute.snapshot.paramMap.get(`id`))
    this.product = await this.psuedoLogicAdmin.getProductById(id);
    this.specNames = await this.psuedoLogicAdmin.getSpecifications();
    this.mainCatList = await this.psuedoLogicAdmin.getCategories("MainCat")
    this.subCatList = await this.psuedoLogicAdmin.getCategories("SubCat")
    this.brandCatList = await this.psuedoLogicAdmin.getCategories("Brand")
    this.specDesc = this.product.specsDescriptions;
    this.specList = await this.psuedoLogicAdmin.makeCurrentSpecList(id)
    this.child.updateNow(this.specList)
  }

  async updateProduct(){
    let httpResponse = await this.psuedoLogicAdmin.putProduct(this.product)
      if(httpResponse != undefined){
      await this.route.navigateByUrl("admin")
    }
  }

  addTooSpecList(spec: number, desc: string){
    let newSpec = new CurrentSpecs(); // a new current spec for list to send to the child list component
    newSpec.specsId = spec;
    newSpec.description = desc;
    this.specDesc.push({specsId: spec, description: desc}) // push to both the products own desc list and the visual component list
    this.specList.push(newSpec)
    this.product.specsDescriptions = this.specDesc;
    this.child.updateNow(this.specList); // signals the specification list to refresh
  }

  onDelete(event: number): void{ // Delete the spec from both the visual and the product lists
    this.specList.splice(this.specList.findIndex(a => a.specsId === event) , 1)
    this.specDesc.splice(this.specDesc.findIndex(a => a.specsId === event) , 1)
    this.product.specsDescriptions = this.specDesc;
    this.child.updateNow(this.specList);
  }
}
