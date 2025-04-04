import { Component, OnDestroy, OnInit } from '@angular/core';
import { Whiteboard } from "../store/whiteboard.reducer";
import { ActivatedRoute, Router } from "@angular/router";
import { Store } from "@ngrx/store";
import { getWhiteboards } from "../store/whiteboard.actions";
import { selectCurrentGroupId } from "../../../groups/group-hub/store/group-hub.selectors";
import { distinctUntilChanged, Observable, Subject, takeUntil } from "rxjs";
import { selectWhiteboards } from "../store/whiteboard.selectors";
import { filter, tap } from "rxjs/operators";
import { AppState } from "../../../store/app.reducer";

@Component({
  selector: 'app-whiteboards-list',
  templateUrl: './whiteboards-list.component.html',
  styleUrls: ['./whiteboards-list.component.scss']
})
export class WhiteboardsListComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  whiteboards$: Observable<Whiteboard[]>;

  constructor(private store: Store<AppState>, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.store.select(selectCurrentGroupId)
      .pipe(
        takeUntil(this.destroyed$),
        filter(id => !!id),
        distinctUntilChanged()
      ).subscribe(groupId => {
        this.store.dispatch(getWhiteboards({groupId}))
      });

    this.whiteboards$ = this.store.select(selectWhiteboards)
      .pipe(
        takeUntil(this.destroyed$),
        distinctUntilChanged(),
        tap(res => {
          console.log('res: ', res);
        })
      );
  }

  navigateToWhiteboard(id: number): void {
    this.router.navigate([id], { relativeTo: this.activatedRoute });
  }

  openCreateForm() {
    this.router.navigate(['create'], { relativeTo: this.activatedRoute });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
