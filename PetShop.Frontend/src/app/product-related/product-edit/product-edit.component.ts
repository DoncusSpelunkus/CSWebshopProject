import {Component, OnInit, ViewChild} from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { ProductService } from "../../../services/Product.service";
import { Product } from "../../../Entities/Product";
import { Router } from "@angular/router";
import {SpecificationService} from "../../../services/SpecificationService";
import {Specification} from "../../../Entities/specification";
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";
import {find} from "rxjs";

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit { // This component tend to the editing of products
  product: any;
  specList: Specification[] = [];
  newSpecList: CurrentSpecs[] = [];
  specNames: Specification[] = [];
  spec: any;
  specId: number = 0;

  @ViewChild('child') child;

  constructor(private Aroute: ActivatedRoute, private http: ProductService, public route: Router, private specifcationService: SpecificationService) {
    this.product = Product;
  }

  ngOnInit(): void {
    const id =  Number(this.Aroute.snapshot.paramMap.get(`id`))
    this.specifcationService.getSpecifications().subscribe(specifications => {
      this.specNames = specifications

    })
    this.http.GetProductByID(id).subscribe(async productReceived => {
      this.product = productReceived;
      this.newSpecList = productReceived.specsDescriptions;
      await this.attachNames();
      this.child.updateNow(this.newSpecList)
    })
  }

  updateProduct(){
    this.http.UpdateProduct(this.product)
      .subscribe(() => {
        this.route.navigateByUrl('/products');
      });
  }

  onDelete(event: number): void{
    this.newSpecList.splice(this.newSpecList.findIndex(a => a.specsId === event) , 1)
    console.log(this.newSpecList);
    this.child.updateNow(this.newSpecList);
  }

  async attachNames(){ // Attaches names from the clean spec list onto the product specific list
    this.newSpecList.forEach((cspec) => {
      this.specNames.find((nspec) => {
        if(cspec.specsId === nspec.id){
          cspec.specName = nspec.specName;
        }
      })
    })
  }
}
