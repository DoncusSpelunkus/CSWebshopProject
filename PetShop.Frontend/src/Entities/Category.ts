import {Product} from "./Product";

export class Category {
  catName: string = "";
  name?: string;
  id?: number;
  subCategoryID?: number;
  mainCategoryID?: number;
  productList?: Product[];
}
