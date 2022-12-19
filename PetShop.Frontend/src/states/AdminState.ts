import {ProductService} from "../services/Product.service";
import {Product} from "../Entities/Product";
import {SpecificationService} from "../services/SpecificationService";
import {CurrentSpecs} from "../Entities/CurrentSpecs";
import {SpecTemplates} from "../Entities/SpecTemplates";
import {Injectable} from "@angular/core";
import {CategoryService} from "../services/CategoryService";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({ providedIn: 'root' })

export class AdminState { // State class for data manipulation

  productsUnmodified: Product[] = [];
  newSpecList: CurrentSpecs[] = [];
  specNames: SpecTemplates[] = [];
  product: any = Product;


  constructor(private productService: ProductService, private specificationService: SpecificationService,
              private categoryService: CategoryService, private matSnackbar: MatSnackBar) {

  }

  async postProduct(product: Product){
    let dto = {
      name: product.name,
      price: product.price,
      description: product.description,
      imageUrl: product.imageUrl,
      rating: product.averageRating,
      specsDescriptions: product.specsDescriptions,
      mainCategoryID: product.mainCategoryID,
      subCategoryID: product.subCategoryID,
      brandID: product.brandID,
    }
    await this.productService.postProduct(dto)
  }

  async getProducts(){
    this.productsUnmodified = await this.productService.getProducts();
    return this.productsUnmodified;
  }

  async putProduct(product: Product){
    let dto = {
      name: product.name,
      price: product.price,
      description: product.description,
      imageUrl: product.imageUrl,
      mainCategoryID: product.mainCategoryID,
      subCategoryID: product.subCategoryID,
      brandID: product.brandID,
      specsDescriptions: product.specsDescriptions
    }
    let httpResponse = await this.productService.putProduct(dto, product.id);
    if(httpResponse.status > 199 && httpResponse.status < 300 ){
      this.matSnackbar.open("great success")
      return httpResponse;
    }
    return undefined;
  }

  async getProductById(id: number){
    let dto = await this.productService.getProductById(id);
    this.product = dto;
    this.product.mainCategory = dto.mainCategoryID;
    this.product.subCategory = dto.subCategoryID;
    this.product.brand = dto.brandID;
    return this.product;
  }

  async deleteProductById(id: number){
    return await this.productService.deleteProductByID(id);
  }

  async getSpecifications(){
    return await this.specificationService.getSpecifications();
  }

  async deleteSpecificationById(id: number){
    return await this.specificationService.deleteSpecificationById(id);
  }

  async postSpecification(spec: any){
    return await this.specificationService.postSpecification(spec);
  }

  async putSpecification(spec: any){
    let httpresponse = await this.specificationService.putSpecification(spec);
    if(httpresponse.status>199 && httpresponse.status <300) {
      this.matSnackbar.open("Great success","x", {duration: 1000})
      return httpresponse.data
    }
    return undefined;
  }

  async getSpecificationById(id: number){
    let httpresponse = await this.specificationService.getSpecificationByID(id);
    return httpresponse.data;
  }

  async makeCurrentSpecList(id: number){
    this.specNames = await this.specificationService.getSpecifications(); // get the specification template names with ids
    let product = await this.productService.getProductById(id);
    this.newSpecList = product.specsDescriptions; // gets the list descriptions from the product
    this.newSpecList.forEach((cspec) => { // get the specification template names for the decriptions by using id
      this.specNames.find((nspec) => {
        if(cspec.specsId === nspec.id){
          cspec.specName = nspec.specName;
        }
      })
    });
    return this.newSpecList;
  }

  async getCategories(path: string){
    return this.categoryService.getCategories(path);
  }

  async deleteCategoryById(id: number | undefined, path: string){
    if(id && path != ""){
      return await this.categoryService.deleteCategoryByID(id, path);
    }
    else this.matSnackbar.open("Error: Either missing category id or type")
  }

  async postCategory(cat: any, path: string){
    let dto = {
      name: cat.catName
    }
    if(path != ""){
      return this.categoryService.postCategory(dto, path);
    }
    else this.matSnackbar.open("Please select category type")
  }

  async putCategory(dto: any, path: string){
    let httpresponse = await this.categoryService.putCategory(dto, path);
    if(httpresponse.status>199 && httpresponse.status <300) {
      this.matSnackbar.open("Great success","x", {duration: 1000})
      return httpresponse.data
    }
    return undefined;
  }

  async getCategoryById(id: number, path: string){
    return this.categoryService.getCategoryById(id, path);
  }

}
