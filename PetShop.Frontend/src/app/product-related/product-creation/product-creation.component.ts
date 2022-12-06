import { Component, OnInit} from '@angular/core';
import { ProductService} from "../../../services/Product.service";
import { Router} from "@angular/router";
import { CommService } from "../../../services/Commservice";
import { Product } from "../../../Entities/Product";
import { Specification } from "../../../Entities/specification";
import { SpecificationService } from "../../../services/SpecificationService";

@Component({
  selector: 'app-product-creation',
  templateUrl: './product-creation.component.html',
  styleUrls: ['./product-creation.component.scss']
})
export class ProductCreationComponent implements OnInit{
  newProduct: any;
  specList: { SpecsId: number, Description: string }[] = [];
  specNames: Specification[] = [];
  sname: number;
  sdesc: string;
  productDeleteId: number;
  selected: number = 0;


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
    console.log(this.newProduct)
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

  addTooSpecList(spec: number, desc: string){
    this.specList.push({SpecsId: spec, Description: desc})
    console.log(spec)
    this.newProduct.specList = this.specList;
  }

  consoleLog(){
    console.log(this.newProduct)
  }
}
