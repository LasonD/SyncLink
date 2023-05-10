import { Component, OnInit } from '@angular/core';
import { AppState } from "./store/app.reducer";
import { Store } from "@ngrx/store";
import { AutoLoginStart } from "./auth/store/auth.actions";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'synclink-client';

  constructor(private store: Store<AppState>) {
  }

  ngOnInit(): void {
    this.store.dispatch(new AutoLoginStart());
  }
}
