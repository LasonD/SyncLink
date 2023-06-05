import { Component, HostListener } from '@angular/core';
import { AppState } from "./store/app.reducer";
import { Store } from "@ngrx/store";
import { AutoLoginStart } from "./auth/store/auth.actions";
import { debounceTime, Subject } from "rxjs";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private mousePosition = new Subject<{ x: number; y: number }>();
  title = 'synclink-client';
  showHeader = true;

  constructor(private store: Store<AppState>) {
    this.store.dispatch(new AutoLoginStart());

    this.mousePosition.pipe(debounceTime(100)).subscribe(({ y }) => {
      this.showHeader = y < 50;
    });

    setTimeout(() => {
      this.showHeader = false;
    }, 5000);
  }

  @HostListener('window:mousemove', ['$event'])
  onMouseMove(event: MouseEvent) {
    this.mousePosition.next({ x: event.clientX, y: event.clientY });
  }
}
