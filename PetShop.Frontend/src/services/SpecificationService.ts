import { Injectable } from '@angular/core';
import axios from "axios";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpClient} from "@angular/common/http";
import {catchError, Observable} from "rxjs";
import {Specification} from "../Entities/specification";



@Injectable({
  providedIn: 'root'
})
export class SpecificationService {
  apiUrl = 'https://localhost:7143/Specs';

  constructor(private matSnackbar: MatSnackBar, private http: HttpClient) {
  }

  getSpecifications(): Observable<[Specification]>{
    return this.http.get<[Specification]>(this.apiUrl)
  }

  addSpecification(specification: Specification): Observable<Specification>{
    let dto = {
      name: specification.name,
    }
    return this.http.post<Specification>(this.apiUrl, dto);
  }

  updateSpecification(specification: Specification): Observable<Specification>{
    let dto = {
      id: specification.id,
      name: specification.name,
    }
    return this.http.put<Specification>(this.apiUrl+'/'+specification.id, dto)
  }

  deleteSpecificationById(id: any): Observable<Specification> {
    return this.http.delete<Specification>(this.apiUrl+'/' + id)
  }

  getSpecificationByID(id: number): Observable<Specification> {
    return this.http.get<Specification>(this.apiUrl+'/' + id)
  }

}
