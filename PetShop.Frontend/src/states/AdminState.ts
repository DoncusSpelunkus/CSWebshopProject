import {ProductService} from "../services/Product.service";
import {Product} from "../Entities/Product";
import {SpecificationService} from "../services/SpecificationService";
import {CurrentSpecs} from "../Entities/CurrentSpecs";
import {SpecTemplates} from "../Entities/SpecTemplates";
import {Injectable} from "@angular/core";

@Injectable({ providedIn: 'root' })

export class AdminState { // State class for data manipulation

  productsUnmodified: Product[] = [];
  newSpecList: CurrentSpecs[] = [];
  specNames: SpecTemplates[] = [];


  constructor(private productService: ProductService, private specificationService: SpecificationService) {
  }

  async postProduct(product: Product){
    let dto = {
      name: product.name,
      price: product.price,
      description: product.description,
      imageUrl: product.imageUrl,
      rating: product.rating,
      specsDescriptions: product.specsDescriptions,
      mainCategory: product.mainCategory,
      subCategory: product.subCategory,
      brand: product.brand,
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
    return this.productService.getProductById(id);
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
    this.specNames = await this.specificationService.getSpecifications();
    let product = await this.productService.getProductById(id)
    this.newSpecList = product.specsDescriptions;
    this.newSpecList.forEach((cspec) => {
      this.specNames.find((nspec) => {
        if(cspec.specsId === nspec.id){
          cspec.specName = nspec.specName;
        }
      })
    })
    return this.newSpecList;
  }
}
