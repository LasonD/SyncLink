import { Component } from '@angular/core';
import { AppState } from "./store/app.reducer";
import { Store } from "@ngrx/store";
import { AutoLoginStart } from "./auth/store/auth.actions";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'synclink-client';

  constructor(private store: Store<AppState>) {
    console.log('Auto login start');
    this.store.dispatch(new AutoLoginStart());
  }
}
