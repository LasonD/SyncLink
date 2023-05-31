import { Component } from '@angular/core';
import { distinctUntilChanged, Observable, Subject, takeUntil } from "rxjs";
import { Store } from "@ngrx/store";
import { ActivatedRoute, Router } from "@angular/router";
import { selectCurrentGroupId } from "../../groups/group-hub/store/group-hub.selectors";
import { filter, tap } from "rxjs/operators";
import { getWhiteboards } from "../whiteboard/store/whiteboard.actions";
import { selectWhiteboards } from "../whiteboard/store/whiteboard.selectors";
import { WordsChainOverview } from "./store/words-chain.reducer";
import { AppState } from "../../store/app.reducer";
import { getWordsChainGames } from "./store/words-chain.actions";
import { selectWordsChainEntities } from "./store/words-chain.selectors";

@Component({
  selector: 'app-words-chain-list',
  templateUrl: './words-chain-list.component.html',
  styleUrls: ['./words-chain-list.component.scss']
})
export class WordsChainListComponent {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  whiteboards$: Observable<WordsChainOverview[]>;

  constructor(private store: Store<AppState>, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.store.select(selectCurrentGroupId)
      .pipe(
        takeUntil(this.destroyed$),
        filter(id => !!id),
        distinctUntilChanged()
      ).subscribe(groupId => {
      this.store.dispatch(getWordsChainGames({groupId}))
    });

    this.whiteboards$ = this.store.select(selectWordsChainEntities)
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
