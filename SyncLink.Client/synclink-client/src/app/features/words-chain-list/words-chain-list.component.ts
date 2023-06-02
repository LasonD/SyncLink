import { Component } from '@angular/core';
import { distinctUntilChanged, Observable, Subject, takeUntil } from "rxjs";
import { Store } from "@ngrx/store";
import { ActivatedRoute, Router } from "@angular/router";
import { selectCurrentGroupId } from "../../groups/group-hub/store/group-hub.selectors";
import { filter, tap } from "rxjs/operators";
import { WordsChainOverview } from "./store/words-chain.reducer";
import { AppState } from "../../store/app.reducer";
import { getWordsChainGames } from "./store/words-chain.actions";
import { selectAllWordsChainOverviews } from "./store/words-chain.selectors";

@Component({
  selector: 'app-words-chain-list',
  templateUrl: './words-chain-list.component.html',
  styleUrls: ['./words-chain-list.component.scss']
})
export class WordsChainListComponent {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  wordChains$: Observable<WordsChainOverview[]>;

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

    this.wordChains$ = this.store.select(selectAllWordsChainOverviews)
      .pipe(
        takeUntil(this.destroyed$),
        distinctUntilChanged(),
        tap(res => {
          console.log('res: ', res);
        })
      );
  }

  navigateToWordsChain(id: number): void {
    this.router.navigate([id], { relativeTo: this.activatedRoute });
  }

  openCreateForm() {
    this.router.navigate(['create'], { relativeTo: this.activatedRoute });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
