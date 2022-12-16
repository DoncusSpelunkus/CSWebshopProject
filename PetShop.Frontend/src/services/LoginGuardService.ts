import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import jwtDecode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class LoginGuardService  implements CanActivate{

  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let localToken = localStorage.getItem('auth');
    if(localToken){ // checks if the local token exist at all
      let decodToken = jwtDecode(localToken) as Token;
      let currentdate = new Date();
      if(decodToken.exp){
        let expiry = new Date(decodToken.exp * 1000);
        if(currentdate<expiry && decodToken.type === "Admin"){ // checks the role assigned to the token and exp date
          this.router.navigateByUrl("admin") // Redirects if the token has an admin role
          return false;
        }
        else if(currentdate<expiry && decodToken.type === "User") {
          this.router.navigateByUrl("user") // Redirects if the token has a user role
          return false;
        }
      }

    }
    return true; // if the token does not exist, expired or do not have the role of user or admin, go to login.
  }
}
class Token{ // a small expression of the token
  exp?: number;
  type?: string;
}

