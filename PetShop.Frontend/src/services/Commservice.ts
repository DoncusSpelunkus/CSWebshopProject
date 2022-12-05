import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CommService {
  private subjectName = new Subject<any>();

  sendUpdate(boolean: boolean) {
    this.subjectName.next(boolean);
  }

  getUpdate(): Observable<any> {
    console.log("is called")
    return this.subjectName.asObservable();
  }
}
