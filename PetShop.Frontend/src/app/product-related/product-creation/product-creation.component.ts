import {Component, OnInit, ViewChild} from '@angular/core';
import { ProductService} from "../../../services/Product.service";
import { Router} from "@angular/router";
import { CommService } from "../../../services/Commservice";
import { Product } from "../../../Entities/Product";
import { Specification } from "../../../Entities/specification";
import { SpecificationService } from "../../../services/SpecificationService";
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";

@Component({
  selector: 'app-product-creation',
  templateUrl: './product-creation.component.html',
  styleUrls: ['./product-creation.component.scss']
})
export class ProductCreationComponent implements OnInit{
  newProduct: any;
  specList: CurrentSpecs[] = [];
  specNames: Specification[] = [];
  specDesc: {specsId: number, description: string}[] = [];
  sname: number;
  sdesc: string;
  productDeleteId: number;
  selected: number = 0;

  @ViewChild('child') child;

  constructor(private productService: ProductService, public router: Router,
              private commService: CommService, private specifcationService: SpecificationService){
    this.newProduct = new Product;
    this.sname = 0;
    this.sdesc = '';
    this.productDeleteId = 0;
  }

  async ngOnInit() {
    this.specifcationService.getSpecifications().subscribe(specifications => this.specNames = specifications)
  }

  createProduct(){
    this.productService.addProduct(this.newProduct)
      .subscribe(() => {
        this.router.navigateByUrl('/home');
      });
  }


  deleteProduct(id){
    this.productService.DeleteProductByID(id)
      .subscribe(() => {
        this.router.navigateByUrl('/home');
      });
    this.updateList();
  }

  updateList(){
    this.commService.sendUpdate(true)
  }

  onDelete(event: number): void{ // Delete the spec with the id from the mat-card selected in the child component
    this.specList.splice(this.specList.findIndex(a => a.specsId === event) , 1)
    this.child.updateNow(this.specList);
  }

  addTooSpecList(spec: number, desc: string){
    let newSpec = new CurrentSpecs();
    newSpec.specsId = spec;
    newSpec.description = desc;
    this.specDesc.push({specsId: spec, description: desc}) // attachs value on an Array that mirrors the requested field in the post method
    this.newProduct.specsDescriptions = this.specDesc;
    this.specList.push(newSpec)
    this.child.updateNow(this.specList);
  }

  consoleLog(){
    console.log(this.newProduct)
  }
}
