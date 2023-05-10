import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, Subscription } from "rxjs";
import { Injectable, OnDestroy } from "@angular/core";;
import { AppState } from "../../store/app.reducer";
import { Store } from "@ngrx/store";
import { map } from "rxjs/operators";
import { User } from "../user.model";
import { environment } from "../../environments/environment";

@Injectable({providedIn: 'root'})
export class AuthInterceptorService implements HttpInterceptor, OnDestroy {
  baseUrlPlaceholder = '{baseUrl}';
  userSub: Subscription;
  user: User;

  constructor(private store: Store<AppState>) {
    this.userSub = this.store
      .select('auth')
      .pipe(map(state => state.user))
      .subscribe(user => {
        console.log('Got a user: ', user);
        this.user = user;
      })
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this?.user?.token;

    const newReq = req.clone({
      url: req.url.replace(this.baseUrlPlaceholder, environment.apiBaseUrl),
      headers: !!token ? new HttpHeaders().set('Authorization', `Bearer ${token}`) : null,
    });

    return next.handle(newReq);
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }
}
