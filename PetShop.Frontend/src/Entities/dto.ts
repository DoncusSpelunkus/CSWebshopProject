import {appValuePair} from "./valuePair";

export class dto {
  name: string = '';
  price: number = 0;
  description: string = '';
  imageUrl: string = '';
  rating: number = 0;
  mainCategory: number = 0;
  subCategory: number = 0;
  brand: number = 0;
  specNames: any;
  specList: Array<appValuePair> = [];

  constructor(name, price, description, imageUrl, rating, mainCategory, subCategory, brand, specList) {
      this.name = name;
      this.price = price;
      this.description = description;
      this.imageUrl = imageUrl;
      this.rating = rating;
      this.mainCategory = mainCategory;
      this.subCategory = subCategory;
      this.brand = brand;
  }
}
