import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap, tap } from "rxjs/operators";
import { TextPlotEntry, TextPlotGame, TextPlotVote } from "./text-plot-game.reducer";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import * as TextPlotGameActions from './text-plot-game.actions';
import { of } from "rxjs";
import { Page } from "../../../models/pagination.model";
import { ToastrService } from "ngx-toastr";

@Injectable()
export class TextPlotGameEffects {
  constructor(private actions$: Actions, private http: HttpClient, private notificationsService: ToastrService) {}

  getGames$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.getGames),
      mergeMap(({ groupId }) =>
        this.http.get<Page<TextPlotGame>>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/textPlotGames`)
          .pipe(
            map(games => TextPlotGameActions.getGamesSuccess({ games })),
            catchError(err => of(TextPlotGameActions.getGamesFailure({ error: err })))
          )
      )
    )
  );

  getGamesFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.getGamesFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when fetching games.');
        }
      )
    ), {dispatch: false}
  );

  getGameEntries$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.getGameEntries),
      mergeMap(({ gameId, groupId }) =>
        this.http.get<Page<TextPlotEntry>>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/textPlotGames/${gameId}/entries`)
          .pipe(
            map(entries => TextPlotGameActions.getGameEntriesSuccess({ entries })),
            catchError(err => of(TextPlotGameActions.getGameEntriesFailure({ error: err })))
          )
      )
    )
  );

  getGameEntriesFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.getGameEntriesFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when fetching game entries.');
        }
      )
    ), {dispatch: false}
  );

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

  startGameSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.startGameSuccess),
      tap(({ game }) => {
          this.notificationsService.success(`A new Text Plot Game with topic ${game.topic} has been successfully started.`, 'Success');
        }
      )
    ), {dispatch: false}
  );

  startGameFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.startGameFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when starting a game.');
        }
      )
    ), {dispatch: false}
  );

  submitEntry$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.submitEntry),
      mergeMap(({ entry, groupId, gameId }) =>
        this.http.post<TextPlotEntry>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/textPlotGames/${gameId}/entries`, entry)
          .pipe(
            map((entry) => TextPlotGameActions.submitEntrySuccess({ entry })),
            catchError(err => of(TextPlotGameActions.submitEntryFailure({ error: err })))
          )
      )
    )
  );

  submitEntryFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.submitEntryFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when submitting an entry.');
        }
      )
    ), {dispatch: false}
  );

  voteEntry$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.voteEntry),
      mergeMap(({ vote, groupId, gameId, entryId }) =>
        this.http.post<TextPlotVote>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/textPlotGames/${gameId}/entries/${entryId}/votes`, vote)
          .pipe(
            map((vote) => TextPlotGameActions.voteEntrySuccess({ vote })),
            catchError(err => of(TextPlotGameActions.voteEntryFailure({ error: err })))
          )
      )
    )
  );

  voteEntryFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.voteEntryFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when voting an entry.');
        }
      )
    ), {dispatch: false}
  );

  gameStartedExternal$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TextPlotGameActions.gameStartedExternal),
      tap(({ game }) => {
          this.notificationsService.info(`A new Text Plot Game with topic ${game.topic} has been started.`, 'Join your group mates!');
        }
      )
    ), {dispatch: false}
  );
}

export interface SubmitTextPlotVoteData {
  comment: string;
  score: number;
}

export interface SubmitTextPlotEntryData {
  text: string;
}

export interface StartTextPlotGameData {
  topic: string;
}
