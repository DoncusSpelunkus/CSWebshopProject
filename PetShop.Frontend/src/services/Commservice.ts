import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import {Specification} from "../Entities/specification";

@Injectable({ providedIn: 'root' })
export class CommService {
  private subjectName = new Subject<any>();
  private subjectSpecs = new Subject<any>();

  sendUpdate(boolean: boolean) {
    this.subjectName.next(boolean);
  }

  getUpdate(): Observable<any> {
    console.log("is called")
    return this.subjectName.asObservable();
  }

  sendCurrentSpecs(specification: any[]) {
    this.subjectName.next(specification);
  }

  getCurrentSpecs(): Observable<any> {
    return this.subjectName.asObservable();
  }
}
