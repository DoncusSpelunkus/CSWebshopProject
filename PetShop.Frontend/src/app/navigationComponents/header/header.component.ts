import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {Product} from "../../../Entities/Product";
import {SearchState} from "../../../states/SearchState";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  products: Product[] = [];
  name: string = "";

  @Output() emitter = new EventEmitter<string>();

  constructor(private searchState: SearchState) { }

  async ngOnInit() {
    this.products = await this.searchState.getProducts();
  }

  logOut(){
    localStorage.clear()
  }

  emit(keyword){
    this.emitter.emit(keyword);
  }



}
