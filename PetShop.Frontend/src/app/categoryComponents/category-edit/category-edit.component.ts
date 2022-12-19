import { Component, OnInit } from '@angular/core';
import {Category} from "../../../Entities/Category";
import {ActivatedRoute, Router} from "@angular/router";
import {AdminState} from "../../../states/AdminState";

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent implements OnInit {

  category: any = Category;
  path: string = '';

  constructor(private Aroute: ActivatedRoute, private adminState: AdminState, public route: Router) {

  }


  async ngOnInit() { // Gets the category type and id by route
    const id = Number(this.Aroute.snapshot.paramMap.get('id'))
    this.path = String(this.Aroute.snapshot.paramMap.get('path'))
    this.category = await this.adminState.getCategoryById(id, this.path);
    this.category.id = id;
  }

  async updateCat(){
    let data = await this.adminState.putCategory(this.category, this.path);
    if(data != undefined){
      await this.route.navigateByUrl("admin")
    }
  }

}
