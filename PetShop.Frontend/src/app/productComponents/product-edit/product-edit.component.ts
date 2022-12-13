import {Component, OnInit, ViewChild} from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { Product } from "../../../Entities/Product";
import { Router } from "@angular/router";
import {SpecTemplates} from "../../../Entities/SpecTemplates";
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";
import {AdminState} from "../../../states/AdminState";

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit { // This component tend to the editing of products
  product: any;
  newSpecList: CurrentSpecs[] = [];
  specNames: SpecTemplates[] = [];
  spec: any;
  specId: number = 0;

  @ViewChild('child') child;

  constructor(private Aroute: ActivatedRoute, private adminState: AdminState, public route: Router) {
    this.product = Product;
  }

  async ngOnInit() {
    const id = Number(this.Aroute.snapshot.paramMap.get(`id`))
    this.product = await this.adminState.getProductById(id);
    this.newSpecList = await this.adminState.makeCurrentSpecList(id)
    this.child.updateNow(this.newSpecList)
  }

  updateProduct(){
    this.product.specList = this.newSpecList;
    this.adminState.putProduct(this.product);
  }

  onDelete(event: number): void{ // Delete the spec with the id from the mat-card selected in the child component
    this.newSpecList.splice(this.newSpecList.findIndex(a => a.specsId === event) , 1)
    this.child.updateNow(this.newSpecList);
  }
}
