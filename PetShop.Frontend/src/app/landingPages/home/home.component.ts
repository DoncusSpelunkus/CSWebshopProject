import {Component, OnInit, ViewChild} from '@angular/core';
import {PseudoLogicSearch} from "../../../states/PseudoLogicSearch";
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
  currentPrice: number = 0;
  ratingStorage: number = 0;
  ratings: number[] = [1,2,3,4,5];
  @ViewChild('child') child;

  constructor(private pseudoLogicSearch: PseudoLogicSearch) { }

  async ngOnInit() {
    this.mainCatList = await this.pseudoLogicSearch.getCategories("MainCat")
    this.subCatList = await this.pseudoLogicSearch.getCategories("SubCat")
    this.brandCatList = await this.pseudoLogicSearch.getCategories("Brand")
  }

  async send(keyword){
    this.currentName = keyword;
  }

  filter(){ // Sends the updated filters to the logic and calls the child to get the sorted list
    this.pseudoLogicSearch.setCategorySelector(this.categorySelector, this.ratingStorage)
    this.child.catSort();
  }

  clearFilter(){
    this.child.getProducts();
  }



  updateList(event){
    this.pseudoLogicSearch.setCurrentPrice(event.value)
    this.child.priceSort();
  }

}
