import {Injectable} from "@angular/core";
import {ProductService} from "../services/Product.service";
import {CategoryService} from "../services/CategoryService";
import {Product} from "../Entities/Product";
import {Category} from "../Entities/Category";

@Injectable({ providedIn: 'root' })

export class SearchState{

  productsUnmodified: Product[] = [];
  productsModified: Product[] = [];
  categorySelector: any = Category;
  currentPrice: number = 0;

  constructor(private productService: ProductService, private categoryService: CategoryService) {
  }

  async getCategories(path: string){
    return this.categoryService.getCategories(path);
  }

  async getProducts(){
    this.productsUnmodified = await this.productService.getProducts();
    this.productsModified = this.productsUnmodified;
    return this.productsUnmodified;
  }

  setCurrentPrice(currentPrice: number){
    this.currentPrice = currentPrice;
  }

  setCategorySelector(category: Category){
    this.categorySelector = category;
  }

  async catSort(){ // Sorts the unmodified list given the category id's
    if(this.categorySelector.mainCategoryID != undefined){
      this.productsModified = this.productsModified.filter(product => product.mainCategoryID === this.categorySelector.mainCategoryID)
    }
    if(this.categorySelector.subCategoryID != undefined){
      this.productsModified = this.productsModified.filter(product => product.subCategoryID === this.categorySelector.subCategoryID)
    }
    if(this.categorySelector.id != undefined){
      this.productsModified = this.productsModified.filter(product => product.brandID === this.categorySelector.brandID)
    }
    return this.productsModified;
  }

  async priceSort(){
    this.productsModified = this.productsUnmodified;
    return this.productsModified.filter(product => product.price > this.currentPrice)
  }

}
