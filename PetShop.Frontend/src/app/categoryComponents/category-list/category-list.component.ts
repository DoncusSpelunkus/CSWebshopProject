import {Component, OnInit} from '@angular/core';
import {Category} from "../../../Entities/Category";
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {

  mainCatList: Category[] = [];
  subCatList: Category[] = [];
  brandCatList: Category[] = [];

  constructor(private psuedoLogicAdmin: PsuedoLogicAdmin) { }

  ngOnInit(): void {
    this.updateCat("all")
  }

  async updateCat(path: string){ // Receives a call from admin parent to update one or all lists
    if(path === "MainCat") {
      this.mainCatList = await this.psuedoLogicAdmin.getCategories("MainCat");
    }
    if(path === "SubCat"){
      this.subCatList = await this.psuedoLogicAdmin.getCategories("SubCat");
    }
    if(path === "Brand"){
      this.brandCatList = await this.psuedoLogicAdmin.getCategories("Brand");
    }
    if(path === "all"){
      this.mainCatList = await this.psuedoLogicAdmin.getCategories("MainCat");
      this.subCatList = await this.psuedoLogicAdmin.getCategories("SubCat");
      this.brandCatList = await this.psuedoLogicAdmin.getCategories("Brand");
    }

  }

}
