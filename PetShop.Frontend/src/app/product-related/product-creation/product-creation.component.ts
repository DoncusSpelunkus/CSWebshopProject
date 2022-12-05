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
  product: any;
  specList: { spec: number, desc: string }[] = [];
  specNames: Specification[] = [];
  sname: number;
  sdesc: string;
  productDeleteId: number;
  selected: number = 0;


  constructor(private productService: ProductService, public router: Router,
              private commService: CommService, private specifcationService: SpecificationService){
    this.sname = 0;
    this.sdesc = '';
    this.product = Product;
    this.productDeleteId = 0;
  }

  async ngOnInit() {
    this.specifcationService.getSpecifications().subscribe(specifications => this.specNames = specifications)
  }

  createProduct(){
    this.productService.addProduct(this.product)
      .subscribe(() => {
        this.router.navigateByUrl('/admin');
      });
    this.updateList();
  }


  deleteProduct(id){
    this.productService.DeleteProductByID(id)
      .subscribe(() => {
        this.router.navigateByUrl('/admin');
      });
    this.updateList();
  }

  updateList(){
    this.commService.sendUpdate(true)
  }

  addTooSpecList(spec: number, desc: string){
    this.specList.push({spec: spec, desc: desc})
    this.product.specList = this.specList;
  }
}
