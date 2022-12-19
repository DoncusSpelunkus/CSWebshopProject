import {Component, OnInit, ViewChild} from '@angular/core';
import {PseudoLogicSearch} from "../../../states/PseudoLogicSearch";
import {Category} from "../../../Entities/Category";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  currentName: string = '';
  categorySelector: any = Category;
  mainCatList: Category[] = [];
  subCatList: Category[] = [];
  brandCatList: Category[] = [];
  currentPrice: number = 0;
  ratingStorage: number = 0;
  ratings: number[] = [1,2,3,4,5];
  @ViewChild('child') child;

  constructor(private searchState: PseudoLogicSearch) { }

  async ngOnInit() {
    this.mainCatList = await this.searchState.getCategories("MainCat")
    this.subCatList = await this.searchState.getCategories("SubCat")
    this.brandCatList = await this.searchState.getCategories("Brand")
  }

  async send(keyword){
    this.currentName = keyword;
  }

  filter(){
    this.searchState.setCategorySelector(this.categorySelector, this.ratingStorage)
    this.child.catSort();
  }

  clearFilter(){
    this.child.getProducts();
  }



  updateList(event){
    this.searchState.setCurrentPrice(event.value)
    this.child.priceSort();
  }

}
