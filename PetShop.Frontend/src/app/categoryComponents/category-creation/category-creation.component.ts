import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {PsuedoLogicAdmin} from "../../../states/PsuedoLogicAdmin";
import {Category} from "../../../Entities/Category";

@Component({
  selector: 'app-category-creation',
  templateUrl: './category-creation.component.html',
  styleUrls: ['./category-creation.component.scss']
})

export class CategoryCreationComponent implements OnInit {

  category: any = Category;

  categoryDeleteId?: number;

  selectedDelete: string = "";

  selected: string = "";

  @Output()
  change: EventEmitter<string> = new EventEmitter<string>();

  constructor(private psuedoLogicAdmin: PsuedoLogicAdmin) { }


  ngOnInit(): void {
  }

  // commits the category change and emits a call to refresh the given category list
  async postCategory(path: string) {
    await this.psuedoLogicAdmin.postCategory(this.category, path)
    this.change.emit(path)
  }

  async deleteCategoryById(path: string) {
    await this.psuedoLogicAdmin.deleteCategoryById(this.categoryDeleteId, path)
    this.change.emit(path)
  }
}
