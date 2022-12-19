import {Injectable} from "@angular/core";
import {ProductService} from "../services/Product.service";
import {CategoryService} from "../services/CategoryService";
import {Product} from "../Entities/Product";
import {Category} from "../Entities/Category";

@Injectable({ providedIn: 'root' })

export class PseudoLogicSearch {

  productsUnmodified: Product[] = [];
  productsModified: Product[] = [];
  priceModified: Product[] = [];
  categorySelector: any = Category;
  currentPrice: number = 0;
  ratingStorage: number = 0;

  constructor(private productService: ProductService, private categoryService: CategoryService) {
  }

  async getCategories(path: string){
    return this.categoryService.getCategories(path);
  }

  async getProducts(){ // sets both a list to store of ALL products and makes a duplicate for manipulation
    this.productsUnmodified = await this.productService.getProducts();
    this.productsModified = this.productsUnmodified;
    return this.productsUnmodified;
  }

  setCurrentPrice(currentPrice: number){
    this.currentPrice = currentPrice;
  }

  setCategorySelector(category: Category, rating: number){
    this.categorySelector = category;
    this.ratingStorage = rating;
  }

  async catSort(){ // Sorts the unmodified list given the category id's
    this.productsModified = this.productsUnmodified;
    if(this.categorySelector.mainCategoryID != undefined){
      this.productsModified = this.productsModified.filter(product => product.mainCategoryID === this.categorySelector.mainCategoryID)
    }
    if(this.categorySelector.subCategoryID != undefined){
      this.productsModified = this.productsModified.filter(product => product.subCategoryID === this.categorySelector.subCategoryID)
    }
    if(this.categorySelector.id != undefined){
      this.productsModified = this.productsModified.filter(product => product.brandID === this.categorySelector.brandID)
    }
    if(this.categorySelector.rating != 0){
      this.productsModified = this.productsModified.filter(product => product.averageRating >= this.ratingStorage)
    }
    return this.productsModified;
  }

  async priceSort(){ // sorts list by price
    this.priceModified = this.productsModified;
    return this.priceModified.filter(product => product.price > this.currentPrice)
  }

}
