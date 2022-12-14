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
      rating: product.rating,
      specsDescriptions: product.specsDescriptions,
      mainCategoryID: product.mainCategory,
      subCategoryID: product.subCategory,
      brandID: product.brand,
    }
    await this.productService.postProduct(dto)
  }

  async getProducts(){
    this.productsUnmodified = await this.productService.getProducts();
    return this.productsUnmodified;
  }

  async putProduct(product: Product){
    let dto = {
      id: product.id,
      name: product.name,
      price: product.price,
      description: product.description,
      imageUrl: product.imageUrl,
      rating: product.rating,
      mainCategory: product.mainCategory,
      subCategory: product.subCategory,
      brand: product.brand
    }
    return await this.productService.putProduct(dto);
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
    return this.productService.deleteProductByID(id);
  }

  async getSpecifications(){
    return this.specificationService.getSpecifications();
  }

  async deleteSpecificationById(id: number){
    return this.specificationService.deleteSpecificationById(id);
  }

  async postSpecification(spec: any){
    return this.specificationService.postSpecification(spec);
  }

  async putSpecification(spec: any){
    return this.specificationService.putSpecification(spec);
  }

  async getSpecificationById(id: number){
    return this.specificationService.getSpecificationByID(id);
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

  async postCategory(dto: any, path: string){
    if(path != ""){
      return this.categoryService.postCategory(dto, path);
    }
    else this.matSnackbar.open("Please select category type")
  }

  async putCategory(dto: any, path: string){
    return this.categoryService.putCategory(dto, path);
  }

  async getCategoryById(id: number, path: string){
    return this.categoryService.getCategoryById(id, path);
  }

}
