import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map, take } from "rxjs/operators";
import { Store } from "@ngrx/store";
import { AppState } from "../../store/app.reducer";
import { AutoLoginStart } from "../store/auth.actions";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router,
              private store: Store<AppState>) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    this.store.dispatch(new AutoLoginStart());

    return  this.store.select('auth').pipe(
      map(state => state.user),
      take(1),
      map(user => {
        return !!user?.token ? true : this.router.createUrlTree(['/auth']);
      }));
  }
}
