import {Order} from "./Order";

export class OrderProduct { // product template
  uniqueId?: number;
  listOfUniqueId: Order[] = [];
  time?: string;
}
