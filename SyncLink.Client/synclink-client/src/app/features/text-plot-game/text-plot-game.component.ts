import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  TextPlotEntry,
  TextPlotGame,
  TextPlotGameStats,
  TextPlotGameUserStats,
  TextPlotVote
} from "./store/text-plot-game.reducer";
import { VoteModalComponent } from "./vote-modal/vote-modal.component";
import { MatDialog } from "@angular/material/dialog";
import { AppState } from "../../store/app.reducer";
import { Store } from "@ngrx/store";
import { ActivatedRoute } from "@angular/router";
import * as TextPlotGameActions from "./store/text-plot-game.actions";
import { selectCurrentGroupId } from "../../groups/group-hub/store/group-hub.selectors";
import {
  distinctUntilChanged,
  mergeMap,
  Observable,
  Subject,
  takeUntil,
  withLatestFrom
} from "rxjs";
import { filter, map, take } from "rxjs/operators";
import {
  selectEntriesByGameId, selectSelectedGameVotingTimerProgress,
  selectSelectedTextPlotGame,
  selectSelectedTextPlotGameId, selectVotesByEntryId
} from "./store/text-plot-game.selectors";
import { revokeVote, submitEntry, voteEntry } from "./store/text-plot-game.actions";
import { selectUserId } from "../../auth/store/auth.selectors";
import { StatsModalComponent } from "./stats-modal/stats-modal.component";

@Component({
  selector: 'app-text-plot-game',
  templateUrl: './text-plot-game.component.html',
  styleUrls: ['./text-plot-game.component.scss'],
  providers: [MatDialog]
})
export class TextPlotGameComponent implements OnInit, OnDestroy {
  currentUserId$ = this.store.select(selectUserId);
  destroyed$: Subject<boolean> = new Subject<boolean>();
  gameId$ = this.activatedRoute.paramMap.pipe(map(p => +p.get('textPlotGameId')), filter(id => !!id));
  game$: Observable<TextPlotGame> = this.store.select(selectSelectedTextPlotGame).pipe(takeUntil(this.destroyed$));
  entries$ = this.gameId$.pipe(takeUntil(this.destroyed$), mergeMap(gameId => this.store.select(selectEntriesByGameId(gameId))));
  votingTimerProgress$ = this.store.select(selectSelectedGameVotingTimerProgress).pipe(takeUntil(this.destroyed$));
  committedEntries$ = this.entries$.pipe(map(entries => entries.filter(e => e.isCommitted)));
  uncommittedEntries$ = this.entries$.pipe(map(entries => entries.filter(e => !e.isCommitted)));
  votes$: Observable<TextPlotVote[]>;
  canAddEntry$ = this.uncommittedEntries$.pipe(withLatestFrom(this.currentUserId$), map((([entries, userId]) => entries.some(e => e.id === userId))));

  newEntryText: string;

  constructor(private dialog: MatDialog,
              private store: Store<AppState>,
              private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
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
  }

  openVoteModal(entry: TextPlotEntry): void {
    const dialogRef = this.dialog.open(VoteModalComponent, {
      width: '620px',
      data: {entry: entry}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result.isConfirmed) {
        return;
      }

      this.store.select(selectCurrentGroupId)
        .pipe(take(1), withLatestFrom(this.store.select(selectSelectedTextPlotGameId)))
        .subscribe(([groupId, gameId]) => {
          if (result.isRevocation) {
            this.store.dispatch(revokeVote({
              entryId: entry.id, gameId: gameId, groupId: groupId
            }));
          } else {
            this.store.dispatch(voteEntry({
              entryId: entry.id, gameId: gameId, groupId: groupId, vote: {
                comment: result.comment,
                score: result.score
              }
            }));
          }
        })
    });
  }

  showStats() {
    const userStats1: TextPlotGameUserStats = {
      userId: 1,
      username: "john_doe",
      entriesCommittedCount: 5,
      entriesSubmittedCount: 3,
      wordsCommittedCount: 20,
      wordsSubmittedCount: 15,
      totalReceivedScore: 100,
      votesLeftCount: 2,
      commentsReceived: ["Great job!", "Keep it up!"]
    };

    const userStats2: TextPlotGameUserStats = {
      userId: 2,
      username: "jane_smith",
      entriesCommittedCount: 7,
      entriesSubmittedCount: 4,
      wordsCommittedCount: 25,
      wordsSubmittedCount: 20,
      totalReceivedScore: 80,
      votesLeftCount: 3,
      commentsReceived: ["Well done!", "Impressive work!"]
    };

    const gameStats: TextPlotGameStats = {
      gameId: 23,
      groupId: 1,
      topic: "Summertime Activities",
      entriesCommittedCount: 12,
      wordsCommittedCount: 60,
      userStats: [userStats1, userStats2]
    };

    const dialogRef = this.dialog.open(StatsModalComponent, {
      width: '50%',
      data: gameStats
    });
  }

  canVote(entry: TextPlotEntry) {
    return this.currentUserId$.pipe(take(1), map(userId => entry.userId !== userId));
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

  getCompleteText() {
    return this.committedEntries$
      .pipe(take(1), map(e => e.map(x => x.text).join(' ')));
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
