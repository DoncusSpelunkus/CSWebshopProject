import {Component, OnInit} from '@angular/core';
import jwtDecode from "jwt-decode";
import {User} from "../../../Entities/User";
import {PseudoLogicUser} from "../../../states/PseudoLogicUser";

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss']
})
export class RatingComponent implements OnInit {

  rating: number = 1;
  starCount: number = 5;
  ratingArr: number[] = []
  productId: number = 0;

  constructor(private userState: PseudoLogicUser) {
  }


  ngOnInit() {
    for (let index = 0; index < this.starCount; index++) {
      this.ratingArr.push(index);
    }
  }

  setProductId(productId: number){
    this.productId = productId;
  }

  onClick(rating:number) {
    this.rating = rating;
    return false;
  }

  showIcon(index:number) {
    if (this.rating >= index + 1) {
      return 'star';
    } else {
      return 'star_border';
    }
  }

  async giveRating(){
    let localToken = localStorage.getItem('auth');
    if(localToken) {
      let decodedToken = jwtDecode(localToken) as User
      if(decodedToken.id){
        await this.userState.giveRating(this.rating, decodedToken.id, this.productId)
      }
    }
  }
}
