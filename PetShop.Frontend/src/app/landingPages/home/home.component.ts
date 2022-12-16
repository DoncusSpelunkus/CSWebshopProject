import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {SearchState} from "../../../states/SearchState";
import {Category} from "../../../Entities/Category";

class MatSliderDragEvent {
}

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
