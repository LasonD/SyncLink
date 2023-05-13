import { Injectable } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RouteService {
  private routeChangeSubject: Subject<ActivatedRoute> = new Subject<ActivatedRoute>();
  public routeChange$: Observable<ActivatedRoute> = this.routeChangeSubject.asObservable();

  constructor(private router: Router) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.routeChangeSubject.next(this.router.routerState.root);
      });
  }
}
