import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from "rxjs";
import { Router } from "@angular/router";
import { AppState } from "../store/app.reducer";
import { Store } from "@ngrx/store";
import { Logout } from "../auth/store/auth.actions";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  isLoggedIn = false;

  constructor(private store: Store<AppState>,
              private router: Router) {

  }

  ngOnInit(): void {
    this.store.select('auth')
      .pipe(takeUntil(this.destroyed$))
      .subscribe(state => {
        this.isLoggedIn = !!state?.user?.token;
      });
  }

  ngOnDestroy(): void {
    this.destroyed$.next(true);
  }

  logOut() {
    this.store.dispatch(new Logout());
    this.router.navigate(['/auth']);
  }
}
