import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap } from "rxjs/operators";
import { TextPlotEntry, TextPlotGame } from "./text-plot-game.reducer";
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
        this.http.post<TextPlotGame>(`${environment.apiBaseUrl}/textPlotGame/start`, { gameId: game.id, groupId })
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
      mergeMap(({ entry }) =>
        this.http.post<TextPlotEntry>(`${environment.apiBaseUrl}/textPlotGame/submitEntry`, entry)
          .pipe(
            map(entry => TextPlotGameActions.submitEntrySuccess({ entry })),
            catchError(err => of(TextPlotGameActions.submitEntryFailure({ error: err })))
          )
      )
    )
  );

  voteEntry$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.voteEntry),
      mergeMap(({ vote }) =>
        this.http.post(`${environment.apiBaseUrl}/textPlotGame/voteEntry`, vote)
          .pipe(
            map(() => TextPlotGameActions.voteEntrySuccess({ vote })),
            catchError(err => of(TextPlotGameActions.voteEntryFailure({ error: err })))
          )
      )
    )
  );

  endGame$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.endGame),
      mergeMap(({ gameId }) =>
        this.http.post(`${environment.apiBaseUrl}/textPlotGame/endGame`, { gameId })
          .pipe(
            map(() => TextPlotGameActions.endGameSuccess()),
            catchError(err => of(TextPlotGameActions.endGameFailure({ error: err })))
          )
      )
    )
  );
}
