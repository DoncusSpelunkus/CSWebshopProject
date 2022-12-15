import {Component, OnInit, ViewChild} from '@angular/core';
import {SearchState} from "../../../states/SearchState";
import {Category} from "../../../Entities/Category";

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
  @ViewChild('child') child;

  constructor(private searchState: SearchState) { }

  async ngOnInit() {
    this.mainCatList = await this.searchState.getCategories("MainCat")
    this.subCatList = await this.searchState.getCategories("SubCat")
    this.brandCatList = await this.searchState.getCategories("Brand")
  }

  async send(keyword){
    this.currentName = keyword;
  }

  filter(){
    this.searchState.setCategorySelector(this.categorySelector)
    this.child.sortList();
  }

  clearFilter(){
    this.child.getProducts();
  }
}
