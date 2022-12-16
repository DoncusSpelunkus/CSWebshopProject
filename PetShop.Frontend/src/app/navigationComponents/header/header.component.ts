import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {Product} from "../../../Entities/Product";
import {SearchState} from "../../../states/SearchState";
import {Router} from "@angular/router";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  products: Product[] = [];
  name: string = "";

  @Output() emitter = new EventEmitter<string>();

  constructor(private searchState: SearchState, private router: Router) { }

  async ngOnInit() {
    this.products = await this.searchState.getProducts();
  }

  logOut(){
    localStorage.clear()
  }

  emit(keyword){
    this.emitter.emit(keyword);
  }

  changeRoute(){
    this.router.navigateByUrl("cart")
  }

}
