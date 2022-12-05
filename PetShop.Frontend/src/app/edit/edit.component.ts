import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { ProductService } from "../../services/Product.service";
import { Product } from "../../Entities/Product";
import { Router } from "@angular/router";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  product: any;

  constructor(private Aroute: ActivatedRoute, private http: ProductService, public route: Router) {
    this.product = Product;
  }

  ngOnInit(): void {
    const id =  Number(this.Aroute.snapshot.paramMap.get(`id`))
    this.http.GetProductByID(id).subscribe(productReceived => {this.product = productReceived})
  }

  updateProduct(){
    this.http.UpdateProduct(this.product)
      .subscribe(() => {
        this.route.navigateByUrl('/products');
      });
    console.log(this.product)
  }
}
