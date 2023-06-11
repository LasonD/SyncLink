import { Component, OnDestroy, OnInit } from '@angular/core';
import { TextPlotEntry, TextPlotGame, TextPlotVote } from "./store/text-plot-game.reducer";
import { VoteModalComponent } from "./vote-modal/vote-modal.component";
import { MatDialog } from "@angular/material/dialog";
import { AppState } from "../../store/app.reducer";
import { Store } from "@ngrx/store";
import { ActivatedRoute } from "@angular/router";
import * as TextPlotGameActions from "./store/text-plot-game.actions";
import { selectCurrentGroupId } from "../../groups/group-hub/store/group-hub.selectors";
import {
  combineLatestWith,
  distinctUntilChanged,
  forkJoin,
  mergeMap,
  Observable,
  Subject,
  takeUntil,
  withLatestFrom
} from "rxjs";
import { combineLatest, filter, map, take, tap } from "rxjs/operators";
import {
  selectEntriesByGameId,
  selectSelectedTextPlotGame,
  selectSelectedTextPlotGameId, selectVotesByEntryId
} from "./store/text-plot-game.selectors";
import { submitEntry, voteEntry } from "./store/text-plot-game.actions";

@Component({
  selector: 'app-text-plot-game',
  templateUrl: './text-plot-game.component.html',
  styleUrls: ['./text-plot-game.component.scss'],
  providers: [MatDialog]
})
export class TextPlotGameComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();
  gameId$: Observable<number> = new Subject<number>();
  game$: Observable<TextPlotGame>;
  entries$: Observable<TextPlotEntry[]>;
  committedEntries$: Observable<TextPlotEntry[]>;
  uncommittedEntries$: Observable<TextPlotEntry[]>;
  votes$: Observable<TextPlotVote[]>;

  newEntryText: string;

  constructor(private dialog: MatDialog,
              private store: Store<AppState>,
              private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.game$ = this.store.select(selectSelectedTextPlotGame).pipe(takeUntil(this.destroyed$));

    this.gameId$ = this.activatedRoute.paramMap
      .pipe(
        map(p => +p.get('textPlotGameId')),
        filter(id => !!id),
      );

    this.store.select(selectCurrentGroupId)
      .pipe(
        filter(id => !!id),
        distinctUntilChanged(),
        withLatestFrom(this.gameId$),
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe(([groupId, gameId]) => {
        this.store.dispatch(TextPlotGameActions.getGameWithEntries({groupId, gameId}))
      });

    this.entries$ = this.gameId$.pipe(
      takeUntil(this.destroyed$),
      mergeMap(gameId => this.store.select(selectEntriesByGameId(gameId)))
    );

    this.committedEntries$ = this.entries$.pipe(map(entries => entries.filter(e => e.isCommitted)));
    this.uncommittedEntries$ = this.entries$.pipe(map(entries => entries.filter(e => !e.isCommitted)));
  }

  openVoteModal(entry: TextPlotEntry): void {
    const dialogRef = this.dialog.open(VoteModalComponent, {
      width: '550px',
      data: {entry: entry}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result.isConfirmed) {
        return;
      }

      this.store.select(selectCurrentGroupId)
        .pipe(take(1), withLatestFrom(this.store.select(selectSelectedTextPlotGameId)))
        .subscribe(([groupId, gameId]) => {
          this.store.dispatch(voteEntry({
            entryId: entry.id, gameId: gameId, groupId: groupId, vote: {
              comment: result.comment,
              score: result.score
            }
          }))
        })
    });
  }

  getVoteCount(id: number): Observable<number> {
    return this.getEntryVotes(id)
      .pipe(map(votes => votes?.length));
  }

  getScore(id: number): Observable<number> {
    return this.getEntryVotes(id)
      .pipe(map(votes => votes.map(v => v.score).reduce((acc, current) => acc + current, 0)));
  }

  getEntryVotes(id: number): Observable<TextPlotVote[]> {
    return this.store.select(selectVotesByEntryId(id))
      .pipe(take(1));
  }

  addNewEntry() {
    if (!this.newEntryText) {
      return;
    }

    this.store.select(selectCurrentGroupId)
      .pipe(
        take(1),
        withLatestFrom(this.gameId$)
      ).subscribe(([groupId, gameId]) => {
      this.store.dispatch(submitEntry({
        groupId, gameId, entry: {
          text: this.newEntryText
        }
      }));
    });

    this.newEntryText = '';
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
