import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CommService { // Creates a service to facilitate communication between components
  private productListSub = new Subject<any>(); // For requests to update the productlist
  private productEditSub = new Subject<number>() // For request to fill information into edit fields

  requestProductListUpdate(message: boolean) {
    this.productListSub.next(message);
  }

  getProductListUpdateRequest(): Observable<any> {
    return this.productListSub.asObservable();
  }

  requestProductEdit(message: number){
    this.productEditSub.next(message)
  }

  getProductEdit(): Observable<number>{
    return this.productEditSub.asObservable();
  }


}
