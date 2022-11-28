import {Component, OnInit} from '@angular/core';
import {HttpService} from "../services/http.service";
import {appValuePair} from "../Entities/valuePair";
import {MatSelectChange} from "@angular/material/select";

// @ts-ignore
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{


  constructor(private http: HttpService) {
  }

  async ngOnInit() {
  }


  async DeleteProductByID(id: any) {
  }

  async AttachSpecs(spec: number, name: string){
  }

  async ChangeDrop($event: MatSelectChange){
  }
}
