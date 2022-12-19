import {Component, OnInit} from '@angular/core';
import {Category} from "../../../Entities/Category";
import {AdminState} from "../../../states/AdminState";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {

  mainCatList: Category[] = [];
  subCatList: Category[] = [];
  brandCatList: Category[] = [];

  constructor(private adminState: AdminState) { }

  ngOnInit(): void {
    this.updateCat("all")
  }

  async updateCat(path: string){ // Receives a call from admin parent to update one or all lists
    if(path === "MainCat") {
      this.mainCatList = await this.adminState.getCategories("MainCat");
    }
    if(path === "SubCat"){
      this.subCatList = await this.adminState.getCategories("SubCat");
    }
    if(path === "Brand"){
      this.brandCatList = await this.adminState.getCategories("Brand");
    }
    if(path === "all"){
      this.mainCatList = await this.adminState.getCategories("MainCat");
      this.subCatList = await this.adminState.getCategories("SubCat");
      this.brandCatList = await this.adminState.getCategories("Brand");
    }

  }

}
