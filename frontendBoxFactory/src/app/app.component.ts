import {Component, OnInit} from '@angular/core';
import {HttpService} from "../services/http.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  boxLength: number = 0;
  boxWidth: number = 0;
  boxHeight: number = 0;
  boxes: any;

  constructor(private http: HttpService) {

  }

  async ngOnInit() {
    const boxes = await this.http.GetBoxes();
    this.boxes = boxes;
  }

  async createBox(){
    let dto = {
      length: this.boxLength,
      width: this.boxWidth,
      height: this.boxHeight,
    }
    const result = await this.http.createBox(dto);
    this.boxes.push(result);
  }

  async deleteBox(manFacId: any) {
    const box = await this.http.deleteBox(manFacId);
    this.boxes = this.boxes.filter(b => b.manFacId != box.manFacId)
  }
}
