import axios from "axios";
import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import jwtDecode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})

export class AuthGuardService implements CanActivate{

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let localToken = localStorage.getItem('auth');
    return !!localToken;
  }

  canActivateAdmin(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let localToken = localStorage.getItem('auth');
    if(localToken){
      let decodToken = jwtDecode(localToken) as Token;
      let currentdate = new Date();
      if(decodToken.exp){
        let expiry = new Date(decodToken.exp * 1000);
        if(currentdate<expiry && decodToken.role == "admin"){
          return true;
        }
      }
    }
    return false;
  }


}
class Token{
  exp?: number;
  role?: string;
}
