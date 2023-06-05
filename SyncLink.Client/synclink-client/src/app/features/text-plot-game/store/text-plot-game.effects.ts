import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap } from "rxjs/operators";
import { TextPlotEntry, TextPlotGame, TextPlotVote } from "./text-plot-game.reducer";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import * as TextPlotGameActions from './text-plot-game.actions';
import { of } from "rxjs";

@Injectable()
export class TextPlotGameEffects {
  constructor(private actions$: Actions, private http: HttpClient) {}

  startGame$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.startGame),
      mergeMap(({ game, groupId }) =>
        this.http.post<TextPlotGame>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/textPlotGames`, game)
          .pipe(
            map(game => TextPlotGameActions.startGameSuccess({ game })),
            catchError(err => of(TextPlotGameActions.startGameFailure({ error: err })))
          )
      )
    )
  );

  submitEntry$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.submitEntry),
      mergeMap(({ entry, groupId, gameId }) =>
        this.http.post<TextPlotEntry>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/textPlotGames`, entry)
          .pipe(
            map((entry) => TextPlotGameActions.submitEntrySuccess({ entry })),
            catchError(err => of(TextPlotGameActions.submitEntryFailure({ error: err })))
          )
      )
    )
  );

  voteEntry$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.voteEntry),
      mergeMap(({ vote, groupId, gameId, entryId }) =>
        this.http.post<TextPlotVote>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/textPlotGames/${gameId}/entries/${entryId}`, vote)
          .pipe(
            map((vote) => TextPlotGameActions.voteEntrySuccess({ vote })),
            catchError(err => of(TextPlotGameActions.voteEntryFailure({ error: err })))
          )
      )
    )
  );

  // endGame$ = createEffect(() =>
  //   this.actions$.pipe(
  //     ofType(TextPlotGameActions.endGame),
  //     mergeMap(({ gameId }) =>
  //       this.http.post(`${environment.apiBaseUrl}/textPlotGame/endGame`, { gameId })
  //         .pipe(
  //           map(() => TextPlotGameActions.endGameSuccess()),
  //           catchError(err => of(TextPlotGameActions.endGameFailure({ error: err })))
  //         )
  //     )
  //   )
  // );
}

export interface SubmitTextPlotVoteData {
  comment: string;
}

export interface SubmitTextPlotEntryData {
  text: string;
}

export interface StartTextPlotGameData {
  topic: string;
}
